using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeAnalyzer.Domain.Models.Users;

namespace TimeAnalyzer.Core.Interfaces
{
    public interface IUserService
    {
        User GetCurrentUser();
    }
}
