using Microsoft.AspNetCore.Http;
using System;
using TimeAnalyzer.Core.Interfaces;
using TimeAnalyzer.Domain.Models.Users;

namespace TimeAnalyzer.Core.Users
{
    public class UserService : IUserService
    {
        public User GetCurrentUser()
        {
            throw new NotImplementedException();
        }
    }
}
