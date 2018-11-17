using System;
using System.Collections.Generic;

namespace TimeAnalyzer.Domain.Models.Users
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string Token { get; set; }

        public ICollection<TimeReport> TimeReports { get; set; }
    }
}
