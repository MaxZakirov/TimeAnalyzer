using System;
using System.Collections.Generic;
using TimeAnalyzer.Core.Static;
using TimeAnalyzer.Domain.Interfaces;
using TimeAnalyzer.Domain.Models;

namespace TimeAnalyzer.Core.TimeReports.UpdateStrategy
{
    public abstract class TimeReportUpdateStrategy
    {
        protected readonly IEnumerable<TimeReport> dateTimeReports;
        protected readonly TimeReport newTimeReport;

        public TimeReportUpdateStrategy(
            IEnumerable<TimeReport> dateTimeReports,
            TimeReport newTimeReport)
        {
            this.dateTimeReports = dateTimeReports;
            this.newTimeReport = newTimeReport;
        }

        public void Update()
        {
            PrepareDateTimeReportsToValidation();

            var timeDurationTooBig = !ReportsTimeConsistencyChecker.CheckDayConsistency(dateTimeReports);
            if (timeDurationTooBig)
            {
                throw new Exception();
            }

            UpdateTimeReportDataInRepository();
        }

        public abstract void PrepareDateTimeReportsToValidation();

        public abstract void UpdateTimeReportDataInRepository();
    }
}
