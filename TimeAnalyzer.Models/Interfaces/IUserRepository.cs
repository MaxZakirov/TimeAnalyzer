using System.Threading.Tasks;
using TimeAnalyzer.Domain.Models.Users;

namespace TimeAnalyzer.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmail(string email);

        Task<User> GetByName(string name);
    }
}
