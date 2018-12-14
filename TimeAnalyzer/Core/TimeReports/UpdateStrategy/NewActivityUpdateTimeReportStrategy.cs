using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeAnalyzer.Domain.Interfaces;
using TimeAnalyzer.Domain.Models;

namespace TimeAnalyzer.Core.TimeReports.UpdateStrategy
{
    public class NewActivityUpdateTimeReportStrategy : TimeReportUpdateStrategy
    {
        protected readonly ITimeReportRepository timeReportRepository;

        public NewActivityUpdateTimeReportStrategy(
            ITimeReportRepository timeReportRepository,
            IEnumerable<TimeReport> dateTimeReports,
            TimeReport newTimeReport)
            : base(
                  dateTimeReports,
                  newTimeReport)
        {
            this.timeReportRepository = timeReportRepository;
        }

        public override void PrepareDateTimeReportsToValidation()
        {
            dateTimeReports.ToList().Add(newTimeReport);
        }

        public override void UpdateTimeReportDataInRepository()
        {
            timeReportRepository.Update(newTimeReport);
        }
    }
}
