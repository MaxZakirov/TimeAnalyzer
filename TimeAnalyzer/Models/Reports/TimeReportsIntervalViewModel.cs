using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeAnalyzer.Models.Reports
{
    public class TimeReportsIntervalViewModel
    {
        public TimeReportsIntervalViewModel(
            IEnumerable<ReportViewModel> reports,
            string startDate,
            string endDate)
        {
            Reports = reports;
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public IEnumerable<ReportViewModel> Reports { get; set; }

        public string startDate { get; set; }

        public string endDate { get; set; }
    }
}
