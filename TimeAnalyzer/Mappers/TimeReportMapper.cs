using System;
using TimeAnalyzer.Core.Static;
using TimeAnalyzer.Domain.Models;
using TimeAnalyzer.Models;

namespace TimeAnalyzer.Mappers
{
    public static class TimeReportMapper
    {
        public static TimeReport ToTimeReport(this TimeReportViewModel timeReportView, int userId)
        {
            DateTime date = TimeConverter.ToDateTime(timeReportView.Date);
            return new TimeReport(
                timeReportView.Id,
                date,
                timeReportView.Duration,
                timeReportView.ActivityId,
                timeReportView.Activity,
                userId);
        }
    }
}
