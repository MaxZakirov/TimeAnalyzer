using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using TimeAnalyzer.Domain.Models.Users;
using TimeAnalyzer.Models;

namespace TimeAnalyzer.Core.Interfaces
{
    public interface IUserManager
    {
        //Task<User> ValidateAsync(string loginTypeCode, string identifier, string secret);
        Task SignInAsync(HttpContext httpContext, UserLoginModel user);

        Task Logout(HttpContext httpContext);
    }
}
