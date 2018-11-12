using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using TimeAnalyzer.Domain.Models.Users;

namespace TimeAnalyzer.Core.Interfaces
{
    public interface IUserManager
    {
        Task<User> ValidateAsync(string loginTypeCode, string identifier, string secret);

        void SignInAsync(HttpContext httpContext, User user, bool isPersistent = false);
    }
}
