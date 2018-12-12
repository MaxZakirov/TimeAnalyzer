using System;
using TimeAnalyzer.Core.Static;
using TimeAnalyzer.Domain.Models;
using TimeAnalyzer.Models.Reports;

namespace TimeAnalyzer.Mappers
{
    public static class TimeReportMapper
    {
        public static TimeReport ToTimeReport(this DayTimeReportViewModel timeReportView, int userId)
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

        public static DayTimeReportViewModel ToViewTimeReport(this TimeReport timeReport)
        {
            string stringDate = TimeConverter.ToJSONString(timeReport.Date);
            return new DayTimeReportViewModel(
                timeReport.Id,
                timeReport.Duration,
                timeReport.ActivityId,
                timeReport.Activity,
                stringDate);
        }
    }
}
