using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeAnalyzer.Models.Reports;

namespace TimeAnalyzer.Core.TimeReports
{
    public interface ITimeReportService
    {
        Task<IEnumerable<DayTimeReportViewModel>> GetAllUserTimeReports();

        Task<int> AddTimeReport(DayTimeReportViewModel viewModel);

        Task Update(DayTimeReportViewModel viewModel);

        void DeleteTimeReport(int timeReportId);

        void SetUserName(string userName);

        Task<IEnumerable<DayTimeReportViewModel>> GetDayTimeReportAsync(string stringDate);

        Task<TimeReportsIntervalViewModel> GetTimeReportsInInterval(string startDate, string endDate);

        Task<TimeReportsIntervalViewModel> GetTimeReportsInInterval(DateTime startDate, DateTime endDate);

        Task<int> AddTimeReportFromIOT(IOTViewModel viewModel);
    }
}
