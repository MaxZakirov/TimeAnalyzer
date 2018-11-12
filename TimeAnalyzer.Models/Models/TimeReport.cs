using System;
using TimeAnalyzer.Domain.Enums;
using TimeAnalyzer.Domain.Models.Users;

namespace TimeAnalyzer.Domain.Models
{
    public class TimeReport
    {
        public int Id { get; set; }

        public DateTime Day { get; set; }

        public short Duration { get; set; }

        public ActivityType Activity { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
