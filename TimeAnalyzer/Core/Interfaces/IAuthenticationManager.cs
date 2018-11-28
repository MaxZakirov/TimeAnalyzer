using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using TimeAnalyzer.Domain.Models.Users;
using TimeAnalyzer.Models;

namespace TimeAnalyzer.Core.Interfaces
{
    public interface IAuthenticationManager
    {
        Task<User> Authenticate(UserLoginModel user);

        User CheckIn(UserCheckinModel user);

        Task Logout(HttpContext httpContext);
    }
}
