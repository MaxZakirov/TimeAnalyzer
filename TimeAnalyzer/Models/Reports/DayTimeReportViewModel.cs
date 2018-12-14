using TimeAnalyzer.Domain.Models;

namespace TimeAnalyzer.Models.Reports
{
    public class DayTimeReportViewModel : ReportViewModel
    {
        public DayTimeReportViewModel(
            int id,
            long duration,
            int activityId,
            Activity activity,
            string date)
            : base(
                  duration,
                  activityId,
                  activity)
        {
            Date = date;
            Id = id;
        }

        public int Id { get; set; }

        public string Date { get; set; }
    }
}
