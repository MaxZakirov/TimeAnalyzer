using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeAnalyzer.Domain.Interfaces;
using TimeAnalyzer.Domain.Models;

namespace TimeAnalyzer.Core.TimeReports.UpdateStrategy
{
    public class CreateTimeReportUpdateStrategy : NewActivityUpdateTimeReportStrategy
    {
        private int newTimeReportId;

        public CreateTimeReportUpdateStrategy(
            ITimeReportRepository timeReportRepository,
            IEnumerable<TimeReport> dateTimeReports,
            TimeReport newTimeReport)
            : base(
                  timeReportRepository,
                  dateTimeReports,
                  newTimeReport)
        {
        }

        public override void UpdateTimeReportDataInRepository()
        {
            newTimeReportId = timeReportRepository.Add(newTimeReport);
        }

        public int NewTimeReportId => newTimeReportId;
    }
}
