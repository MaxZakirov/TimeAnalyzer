using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeAnalyzer.Core.TimeReports
{
    public class CurrentUserTimeReportServiceFactory : ITimeReportServiceFactory
    {
        private readonly TimeReportService reportService;

        public CurrentUserTimeReportServiceFactory(ITimeReportService reportService)
        {
            this.reportService = (TimeReportService)reportService;
        }

        public ITimeReportService CreateTimeReportService(string username)
        {
            reportService.SetUserName(username);
            return reportService;
        }
    }
}
