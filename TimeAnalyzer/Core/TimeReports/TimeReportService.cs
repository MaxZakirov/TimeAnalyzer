using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeAnalyzer.Domain.Interfaces;
using TimeAnalyzer.Domain.Models;
using TimeAnalyzer.Mappers;
using TimeAnalyzer.Models;

namespace TimeAnalyzer.Core.TimeReports
{
    public class TimeReportService : ITimeReportService
    {
        private const int UserIdIsUnknownValue = -1;
        private readonly ITimeReportRepository timeReportRepository;
        private readonly IActivityRepository activityRepository;
        private readonly IUserRepository userRepository;
        private int userId;
        private string userName;

        public TimeReportService(
            ITimeReportRepository timeReportRepository,
            IActivityRepository activityRepository,
            IUserRepository userRepository
            )
        {
            this.timeReportRepository = timeReportRepository;
            this.activityRepository = activityRepository;
            this.userRepository = userRepository;
            this.userId = UserIdIsUnknownValue;
        }

        public void SetUserName(string userName)
        {
            this.userName = userName;
        }

        public async Task<int> AddTimeReport(TimeReportViewModel viewModel)
        {
            TimeReport timeReport = viewModel.ToTimeReport(await this.GetUserId(this.userName));
            return this.timeReportRepository.Add(timeReport);
        }

        public async Task<IEnumerable<TimeReport>> GetAllUserTimeReports()
        {
            IEnumerable<TimeReport> timeReports = await this.timeReportRepository.GetAllUserReports(await this.GetUserId(this.userName));
            return timeReports;
        }

        private async Task<int> GetUserId(string userName)
        {
            if (userId == UserIdIsUnknownValue)
            {
                var user = await this.userRepository.GetByName(userName);
                this.userId = user.Id;
            }

            return this.userId;
        }
    }
}
