using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeAnalyzer.Core.TimeReports
{
    public interface ITimeReportServiceFactory
    {
        ITimeReportService CreateTimeReportService(string username);
    }
}
