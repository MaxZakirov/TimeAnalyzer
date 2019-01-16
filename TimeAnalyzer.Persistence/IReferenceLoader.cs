using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeAnalyzer.Domain.Interfaces;
using TimeAnalyzer.Domain.Models;

namespace TimeAnalyzer.Persistence
{
    public class ReferenceLoader : IReferenceLoader
    {
        private readonly IActivityRepository activityRepository;
        private readonly IActivityTypeRepository activityTypeRepository;
        private readonly ITimeReportRepository timeReportRepository;
        private IEnumerable<Activity> activities;
        private IEnumerable<ActivityType> activityTypes;

        public ReferenceLoader(
            IActivityRepository activityRepository,
            IActivityTypeRepository activityTypeRepository,
            ITimeReportRepository timeReportRepository)
        {
            this.activityRepository = activityRepository;
            this.activityTypeRepository = activityTypeRepository;
            this.timeReportRepository = timeReportRepository;
        }

        public async Task LoadForTimeReports(IEnumerable<TimeReport> timeReports)
        {
            var activities = await GetAllActivities();
            var activityTypes = await GetAllActivityTypes();

            foreach (var r in timeReports)
            {
                r.Activity = activities.First(a => a.Id == r.ActivityId);
                r.Activity.Type = activityTypes.First(t => t.Id == r.Activity.TypeId);
            }

        }

        public async Task<IEnumerable<Activity>> GetAllActivities()
        {
            if(activities==null)
            {
                activities = await activityRepository.GetAll();
            }

            return activities;
        }

        public async Task<IEnumerable<ActivityType>> GetAllActivityTypes()
        {
            if (activityTypes == null)
            {
                activityTypes = await activityTypeRepository.GetAll();
            }

            return activityTypes;
        }
    }
}
