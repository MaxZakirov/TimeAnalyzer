using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeAnalyzer.Domain.Interfaces;
using TimeAnalyzer.Domain.Models;

namespace TimeAnalyzer.Core.TimeReports.UpdateStrategy
{
    public class CreateExistingActivityUpdateTimeReportStrtegy : CreateTimeReportUpdateStrategy
    {
        private readonly TimeReport oldActivityTimeReport;

        public CreateExistingActivityUpdateTimeReportStrtegy(
            ITimeReportRepository timeReportRepository,
            IEnumerable<TimeReport> dateTimeReports,
            TimeReport newTimeReport,
            TimeReport oldActivityTimeReport)
            : base(
                  timeReportRepository,
                  dateTimeReports,
                  newTimeReport)
        {
            this.oldActivityTimeReport = oldActivityTimeReport;
        }

        public override void PrepareDateTimeReportsToValidation()
        {
            oldActivityTimeReport.Duration += newTimeReport.Duration;
        }

        public override void UpdateTimeReportDataInRepository()
        {
            timeReportRepository.Update(oldActivityTimeReport);
            newTimeReportId = oldActivityTimeReport.Id;
        }
    }
}
