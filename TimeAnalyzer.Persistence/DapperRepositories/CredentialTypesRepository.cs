using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeAnalyzer.Domain.Interfaces;
using TimeAnalyzer.Domain.Models.Users;

namespace TimeAnalyzer.Persistence.DapperRepositories
{
    public class CredentialTypesRepository : ICredentialTypesRepository
    {
        private readonly IDapperQueryExecuter<CredentialType> queryExecuter;

        public CredentialTypesRepository(IDapperQueryExecuter<CredentialType> queryExecuter)
        {
            this.queryExecuter = queryExecuter;
        }

        public Task<int> AddAssync(CredentialType entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(CredentialType entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CredentialType> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<CredentialType> GetByCodeAsync(string code)
        {
            string query = $"SELECT Id, Code, Name, Position FROM CredentialTypes WHERE Code = @Code";
            var dbArgs = new DynamicParameters();
            dbArgs.Add("Code",code);
            return await queryExecuter.GetAsync(query, dbArgs);
        }

        public Task<CredentialType> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
