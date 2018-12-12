using TimeAnalyzer.Domain.Models;

namespace TimeAnalyzer.Models.Reports
{
    public class DayTimeReportViewModel : ReportViewModel
    {
        public DayTimeReportViewModel(
            int id,
            short duration,
            int activityId,
            Activity activity,
            string date)
            : base(
                  id,
                  duration,
                  activityId,
                  activity)
        {
            Date = date;   
        }

        public string Date { get; set; }
    }
}
