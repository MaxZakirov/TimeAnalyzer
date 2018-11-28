using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeAnalyzer.Domain.Interfaces;
using TimeAnalyzer.Domain.Models;

namespace TimeAnalyzer.Core.TimeReports
{
    public class TimeReportService : ITimeReportService
    {
        private readonly ITimeReportRepository timeReportRepository;
        private readonly IActivityRepository activityRepository;
        private readonly IUserRepository userRepository;

        public TimeReportService(
            ITimeReportRepository timeReportRepository,
            IActivityRepository activityRepository,
            IUserRepository userRepository
            )
        {
            this.timeReportRepository = timeReportRepository;
            this.activityRepository = activityRepository;
            this.userRepository = userRepository;
        }

        public async Task<IEnumerable<TimeReport>> GetAllUserTimeReports(string userName)
        {
            var user = await this.userRepository.GetByName(userName);
            IEnumerable<TimeReport> timeReports = await this.timeReportRepository.GetAllUserReports(user.Id);

            return timeReports;
        }
    }
}
