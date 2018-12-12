using TimeAnalyzer.Domain.Models;

namespace TimeAnalyzer.Models.Reports
{
    public abstract class ReportViewModel
    {
        public ReportViewModel(
            int id,
            short duration,
            int activityId,
            Activity activity
            )
        {
            Id = id;
            Duration = duration;
            ActivityId = activityId;
            Activity = activity;
        }

        public int Id { get; set; }

        public short Duration { get; set; }

        public int ActivityId { get; set; }

        public Activity Activity { get; set; }
    }
}
