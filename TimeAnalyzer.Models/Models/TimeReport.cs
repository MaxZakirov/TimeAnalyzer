using System;
using TimeAnalyzer.Domain.Enums;
using TimeAnalyzer.Domain.Models.Users;

namespace TimeAnalyzer.Domain.Models
{
    public class TimeReport
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public short Duration { get; set; }

        public int ActivityId { get; set; }

        public Activity Activity { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
