using System;
using System.Collections.Generic;
using System.Linq;
using TimeAnalyzer.Core.Exceptions;
using TimeAnalyzer.Core.Static;
using TimeAnalyzer.Domain.Interfaces;
using TimeAnalyzer.Domain.Models;

namespace TimeAnalyzer.Core.TimeReports.UpdateStrategy
{
    public abstract class TimeReportUpdateStrategy
    {
        protected List<TimeReport> dateTimeReports;
        protected readonly TimeReport newTimeReport;

        public TimeReportUpdateStrategy(
            IEnumerable<TimeReport> dateTimeReports,
            TimeReport newTimeReport)
        {
            this.dateTimeReports = dateTimeReports.ToList();
            this.newTimeReport = newTimeReport;
        }

        public void Update()
        {
            PrepareDateTimeReportsToValidation();

            var timeDurationTooBig = !ReportsTimeConsistencyChecker.CheckDayConsistency(dateTimeReports);
            if (timeDurationTooBig)
            {
                throw new IncorrectInputDateException("The duration of report is too big");
            }

            UpdateTimeReportDataInRepository();
        }

        public abstract void PrepareDateTimeReportsToValidation();

        public abstract void UpdateTimeReportDataInRepository();
    }
}
