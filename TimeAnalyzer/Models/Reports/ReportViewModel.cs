using TimeAnalyzer.Domain.Models;

namespace TimeAnalyzer.Models.Reports
{
    public class ReportViewModel
    {
        public ReportViewModel(
            long duration,
            int activityId,
            Activity activity
            )
        {
            Duration = duration;
            ActivityId = activityId;
            Activity = activity;
        }

        public long Duration { get; set; }

        public int ActivityId { get; set; }

        public Activity Activity { get; set; }
    }
}
