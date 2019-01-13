using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeAnalyzer.Models
{
    public class UserViewModel : UserCheckinModel
    {
        public UserViewModel()
        {

        }

        public UserViewModel(
            int id,
            string name,
            string email
            )
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public int Id { get; set; }
    }
}
