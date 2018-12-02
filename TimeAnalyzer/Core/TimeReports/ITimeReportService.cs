using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeAnalyzer.Domain.Models;
using TimeAnalyzer.Models;

namespace TimeAnalyzer.Core.TimeReports
{
    public interface ITimeReportService
    {
        Task<IEnumerable<TimeReport>> GetAllUserTimeReports();

        int AddTimeReport(TimeReportViewModel viewModel);
    }
}
