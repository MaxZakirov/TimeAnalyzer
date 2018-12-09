namespace TimeAnalyzer.Core.TimeReports
{
    public class TimeReportServiceFactory : ITimeReportServiceFactory
    {
        private readonly TimeReportService reportService;

        public TimeReportServiceFactory(ITimeReportService reportService)
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
