using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeAnalyzer.Core.Static;
using TimeAnalyzer.Domain.Interfaces;
using TimeAnalyzer.Domain.Models;
using TimeAnalyzer.Domain.Models.Users;

namespace TimeAnalyzer.Core.Activities
{
    public class SuggestionsService : ISuggestionsService
    {
        private readonly IActivityService activityService;
        private readonly ITimeReportRepository timeReportRepository;
        private readonly IReferenceLoader referenceLoader;
        private readonly IUserRepository userRepository;
        private DateTime suggestionEndDate;
        private DateTime suggestionStartDate;
        private User currentUser;
        private int actualDaysCount = 14;

        public SuggestionsService(
            IActivityService activityService,
            IUserRepository userRepository,
            ITimeReportRepository timeReportRepository,
            IReferenceLoader referenceLoader
            )
        {
            this.activityService = activityService;
            this.userRepository = userRepository;
            this.timeReportRepository = timeReportRepository;
            this.referenceLoader = referenceLoader;
        }

        public async Task<Dictionary<ActivityType, List<Activity>>> GetSuggestions(string userName, string date)
        {
            SetActualDateInterval(date);
            currentUser = await userRepository.GetByName(userName);
            var lastUserReports = await GetLastUserTimeReports(currentUser);
            var suggestions = await GetPopularSuggestions(lastUserReports.Select(r => r.Activity).Distinct());
            return suggestions;
        }

        private async Task<Dictionary<ActivityType, List<Activity>>> GetPopularSuggestions(IEnumerable<Activity> userActivities)
        {
            var usersFavoriteThings = await GetUserFavoriteActivities(userActivities);

            return GetMostValableActivities(usersFavoriteThings);
        }

        private Dictionary<ActivityType, List<Activity>> GetMostValableActivities(Dictionary<Activity, double> usersFavoriteThings)
        {
            Dictionary<ActivityType, List<Activity>> suggestions = new Dictionary<ActivityType, List<Activity>>();

            foreach (var g in usersFavoriteThings.OrderBy(kv => kv.Value))
            {
                ActivityType key = suggestions.Keys.FirstOrDefault(k => k.Id == g.Key.TypeId);
                if (key == null)
                {
                    suggestions.Add(g.Key.Type, new List<Activity> { g.Key });
                }
                else
                {
                    if (suggestions[key].Count() < 3)
                    {
                        suggestions[key].Add(g.Key);
                    }
                }
            }

            return suggestions;
        }

        private async Task<Dictionary<Activity, double>> GetUserFavoriteActivities(IEnumerable<Activity> currentUserActivities)
        {
            Dictionary<Activity, double> usersFavoriteThings = new Dictionary<Activity, double>();
            var users = await userRepository.GetAll();
            foreach (var user in users)
            {
                if (user.Id == currentUser.Id)
                    continue;

                user.TimeReports = await GetLastUserTimeReports(user);

                double userSimilarFactor = GetUserSimilarFactor(user.TimeReports, currentUserActivities);

                if (userSimilarFactor > 0)
                {
                    IEnumerable<Activity> suggestedActivities = GetSugestionsFromUser(user.TimeReports, currentUserActivities);

                    foreach (var a in suggestedActivities)
                    {
                        var key = usersFavoriteThings.Keys.FirstOrDefault(k => k.Id == a.Id);
                        if (key != null)
                        {
                            usersFavoriteThings[key] += userSimilarFactor;
                        }
                        else
                        {
                            usersFavoriteThings.Add(a, userSimilarFactor);
                        }
                    }
                }
            }

            return usersFavoriteThings;
        }

        private IEnumerable<Activity> GetSugestionsFromUser(IEnumerable<TimeReport> timeReports, IEnumerable<Activity> userActivities)
        {
            List<Activity> result = new List<Activity>();
            timeReports = timeReports.Where(r => !userActivities.Select(u => u.Id).Contains(r.ActivityId));

            var groupedByType = timeReports.GroupBy(k => k.Activity.TypeId, v => v, (k, g) => new { TypeId = k, Reports = g });

            foreach (var type in groupedByType)
            {
                int activityCounter = 0;
                Activity activityToAdd = null;
                var groupedByActivityId = type.Reports.GroupBy(k => k.Activity, v => v, (k, g) => new { Activity = k, Reports = g });
                foreach (var kv in groupedByActivityId)
                {
                    if (kv.Reports.Count() > activityCounter)
                    {
                        activityCounter = kv.Reports.Count();
                        activityToAdd = kv.Activity;
                    }
                }

                result.Add(activityToAdd);
            }

            return result;
        }

        private double GetUserSimilarFactor(IEnumerable<TimeReport> otherUserReports, IEnumerable<Activity> neededActivities)
        {
            double userValue = 0;
            otherUserReports = otherUserReports.Where(a => neededActivities.Select(na => na.Id).Contains(a.ActivityId)).Distinct();
            var groupedByType = otherUserReports.GroupBy(k => k.Activity.TypeId, v => v, (k, g) => new { TypeId = k, Reports = g });
            foreach (var type in groupedByType)
            {
                userValue += (1 * ((double)type.Reports.Count() / (double)actualDaysCount)) / (double)neededActivities.Count();
            }

            return Math.Pow(userValue, 2);
        }

        private async Task<IEnumerable<TimeReport>> GetLastUserTimeReports(User user)
        {
            var lastReports = await timeReportRepository.GetUserReportsInInterval(user.Id, suggestionStartDate, suggestionEndDate);
            await referenceLoader.LoadForTimeReports(lastReports);
            return lastReports.Where(r => r.Activity.Type.ImportanceFactor != 0);
        }

        private void SetActualDateInterval(string date)
        {
            suggestionEndDate = TimeConverter.ToDateTime(date);
            suggestionStartDate = suggestionEndDate.AddDays(-actualDaysCount);
        }
    }
}
