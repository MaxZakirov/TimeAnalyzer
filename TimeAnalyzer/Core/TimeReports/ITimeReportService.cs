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

        Task<IEnumerable<DayTimeReportViewModel>> GetDayTimeReportAsync(string stringDate);
    }
}
