using System.Threading.Tasks;
using TimeAnalyzer.Domain.Models.Users;

namespace TimeAnalyzer.Domain.Interfaces
{
    public interface ICredentialTypesRepository : IRepository<CredentialType>
    {
        Task<CredentialType> GetByCodeAsync(string code);
    }
}
