using System.Threading.Tasks;
using TimeAnalyzer.Domain.Models.Users;

namespace TimeAnalyzer.Domain.Interfaces
{
    public interface IUsersRepository : IRepository<User>
    {
        Task<User> GetByEmail(string name);

    }
}
