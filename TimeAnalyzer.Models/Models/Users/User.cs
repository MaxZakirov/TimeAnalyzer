using System.Collections.Generic;

namespace TimeAnalyzer.Domain.Models.Users
{
    public class User
    {
        public User()
        {

        }

        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }

        public ICollection<TimeReport> TimeReports { get; set; }
    }
}
