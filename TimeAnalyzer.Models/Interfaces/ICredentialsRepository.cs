using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeAnalyzer.Domain.Models.Users;

namespace TimeAnalyzer.Domain.Interfaces
{
    public interface ICredentialsRepository : IRepository<Credential>
    {
        Task<IEnumerable<Credential>> GetCredentialsByTypeAsync(int CredentialTypeId);
    }
}
