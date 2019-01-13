using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeAnalyzer.Core.Exceptions;
using TimeAnalyzer.Core.Static;
using TimeAnalyzer.Core.TimeReports.UpdateStrategy;
using TimeAnalyzer.Domain.Interfaces;
using TimeAnalyzer.Domain.Models;
using TimeAnalyzer.Mappers;
using TimeAnalyzer.Models.Reports;

namespace TimeAnalyzer.Core.TimeReports
{
    public class TimeReportService : ITimeReportService
    {
        private const int UserIdIsUnknownValue = -1;
        private readonly IUnitOfWork unitOfWork;
        private readonly ITimeReportRepository timeReportRepository;
        private readonly IActivityRepository activityRepository;
        private readonly IUserRepository userRepository;
        private readonly IActivityTypeRepository activityTypeRepository;
        private int userId;
        private string userName;

        public TimeReportService(
            IUnitOfWork unitOfWork,
            ITimeReportRepository timeReportRepository,
            IActivityRepository activityRepository,
            IUserRepository userRepository,
            IActivityTypeRepository activityTypeRepository
            )
        {
            this.unitOfWork = unitOfWork;
            this.timeReportRepository = timeReportRepository;
            this.activityRepository = activityRepository;
            this.userRepository = userRepository;
            this.activityTypeRepository = activityTypeRepository;
            userId = UserIdIsUnknownValue;
        }

        public void SetUserName(string userName)
        {
            this.userName = userName;
        }

        public void DeleteTimeReport(int timeReportId)
        {
            timeReportRepository.Remove(timeReportId);
        }

        public async Task<int> AddTimeReport(DayTimeReportViewModel viewModel)
        {
            TimeReport timeReport = viewModel.ToTimeReport(await GetUserId());
            timeReport.Id = GlobalConstants.NullId;
            CreateTimeReportUpdateStrategy timeReportUpdateStrategy = (CreateTimeReportUpdateStrategy)(await this.GetTimeReportUpdateStrategy(timeReport));
            timeReportUpdateStrategy.Update();
            return timeReportUpdateStrategy.NewTimeReportId;
        }

        public async Task<IEnumerable<DayTimeReportViewModel>> GetAllUserTimeReports()
        {
            IEnumerable<TimeReport> timeReports = await timeReportRepository.GetAllUserReports(await GetUserId());
        
    return timeReports.Select(tr => tr.ToViewTimeReport());
        }

        public async Task<IEnumerable<DayTimeReportViewModel>> GetDayTimeReportAsync(string stringDate)
        {
            DateTime date = TimeConverter.ToDateTime(stringDate);
            var timeReports = await timeReportRepository.GetDayUserReports(await GetUserId(), date);
            await LoadActivitiesToTimeReports(timeReports);
            return timeReports.AsParallel().Select(tr => tr.ToViewTimeReport());
        }

        public async Task<TimeReportsIntervalViewModel> GetTimeReportsInInterval(string stringStartDate, string stringEndDate)
        {
            DateTime endDate = TimeConverter.ToDateTime(stringEndDate);
            DateTime startDate = TimeConverter.ToDateTime(stringStartDate);
            if(endDate< startDate)
            {
                throw new IncorrectInputDateException("Start date bigger then end date");
            }

            var timeReports = await timeReportRepository.GetUserReportsInInterval(await GetUserId(), startDate, endDate);
            await LoadActivitiesToTimeReports(timeReports);
            return new TimeReportsIntervalViewModel(this.AgregateTimeReports(timeReports), stringStartDate, stringEndDate);
        }

        public async Task Update(DayTimeReportViewModel viewModel)
        {
            TimeReport newTimeReport = viewModel.ToTimeReport(await GetUserId());
            TimeReportUpdateStrategy timeReportUpdateStrategy = await this.GetTimeReportUpdateStrategy(newTimeReport);
            timeReportUpdateStrategy.Update();
        }

        private async Task<TimeReportUpdateStrategy> GetTimeReportUpdateStrategy(TimeReport newTimeReport)
        {
            var dateTimeRepotrs = await timeReportRepository.GetDayUserReports(newTimeReport.UserId, newTimeReport.Date);
            dateTimeRepotrs = dateTimeRepotrs.Where(r => r.Id != newTimeReport.Id).ToList();

            var sameActivityTimeReport = dateTimeRepotrs.FirstOrDefault(r => r.ActivityId == newTimeReport.ActivityId);

            if (newTimeReport.Id == GlobalConstants.NullId)
            {
                if(sameActivityTimeReport != null)
                {
                    return new CreateExistingActivityUpdateTimeReportStrtegy(timeReportRepository, dateTimeRepotrs, newTimeReport, sameActivityTimeReport);
                }
                return new CreateTimeReportUpdateStrategy(timeReportRepository, dateTimeRepotrs, newTimeReport);
            }

            if (sameActivityTimeReport == null)
                return new NewActivityUpdateTimeReportStrategy(timeReportRepository, dateTimeRepotrs, newTimeReport);

            return new ChangedActivityUpdateTimeReportStrategy(unitOfWork, dateTimeRepotrs, newTimeReport, sameActivityTimeReport);
        }

        private async Task<int> GetUserId()
        {
            if (userId == UserIdIsUnknownValue)
            {
                var user = await userRepository.GetByName(userName);
                userId = user.Id;
            }

            return userId;
        }

        private IEnumerable<ReportViewModel> AgregateTimeReports(IEnumerable<TimeReport> timeReports)
        {
            try
            {
                IEnumerable<ReportViewModel> aggregatedTimeReports =
                timeReports.GroupBy(r => r.ActivityId).Select(reports => new ReportViewModel(
                        reports.Select(r => (int)r.Duration).Sum(),
                        reports.Select(r => r.ActivityId).First(),
                        reports.Select(r => r.Activity).First()
                    )).GroupBy((r) => r.ActivityId).Select(group => group.First());
                var check = aggregatedTimeReports.ToArray();
                return check;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private async Task LoadActivitiesToTimeReports(IEnumerable<TimeReport> timeReports)
        {
            var activities = await activityRepository.GetAll();
            var activitiesTypes = await activityTypeRepository.GetAll();

            foreach(var a in activities)
            {
                a.Type = activitiesTypes.First(t => t.Id == a.TypeId);
            }

            foreach (var tr in timeReports)
            {
                tr.Activity = activities.First(a => a.Id == tr.ActivityId);
            }
        }

        public async Task<int> AddTimeReportFromIOT(IOTViewModel viewModel)
        {
            TimeReport timeReport = viewModel.ToTimeReport(viewModel.UserId);
            CreateTimeReportUpdateStrategy timeReportUpdateStrategy = (CreateTimeReportUpdateStrategy)(await this.GetTimeReportUpdateStrategy(timeReport));
            timeReportUpdateStrategy.Update();
            return timeReportUpdateStrategy.NewTimeReportId;
        }
    }
}
