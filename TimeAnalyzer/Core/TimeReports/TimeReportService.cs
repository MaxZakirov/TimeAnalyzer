using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeAnalyzer.Domain.Interfaces;
using TimeAnalyzer.Domain.Models;
using TimeAnalyzer.Models;

namespace TimeAnalyzer.Core.TimeReports
{
    public class TimeReportService : ITimeReportService
    {
        private readonly ITimeReportRepository timeReportRepository;
        private readonly IActivityRepository activityRepository;
        private readonly IUserRepository userRepository;
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
            this.userName = null;
        }

        public void SetUserName(string userName)
        {
            this.userName = userName;
        }

        public int AddTimeReport(TimeReportViewModel viewModel)
        {
            return 0;
        }

        public async Task<IEnumerable<TimeReport>> GetAllUserTimeReports()
        {
            var user = await this.userRepository.GetByName(this.userName);
            IEnumerable<TimeReport> timeReports = await this.timeReportRepository.GetAllUserReports(user.Id);

            return timeReports;
        }
    }
}
