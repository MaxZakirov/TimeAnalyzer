using System;
using TimeAnalyzer.Domain.Enums;
using TimeAnalyzer.Domain.Models.Users;

namespace TimeAnalyzer.Domain.Models
{
    public class TimeReport
    {
        public TimeReport()
        {

        }

        public TimeReport(
            int id,
            DateTime date,
            short duration,
            int activityId,
            Activity activity,
            int userId
            )
        {
            Id = id;
            Date = date;
            Duration = duration;
            ActivityId = activityId;
            UserId = userId;
        }

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public short Duration { get; set; }

        public int ActivityId { get; set; }

        public Activity Activity { get; set; }

        public int UserId { get; set; }
    }
}
