using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using TimeAnalyzer.Domain.Interfaces;
using TimeAnalyzer.Domain.Models.Users;

namespace TimeAnalyzer.Persistence.DapperRepositories
{
    public class CredentialsRepositoy : ICredentialsRepository
    {
        private readonly IDapperQueryExecuter<Credential> queryExecuter;

        public CredentialsRepositoy(IDapperQueryExecuter<Credential> queryExecuter)
        {
            this.queryExecuter = queryExecuter;
        }

        public async Task<int> AddAssync(Credential entity)
        {
            throw new NotImplementedException();
            //using (IDbConnection connection = this.Connection)
            //{
            //    string query = "";
            //    connection.Open();
            //    var result = await connection.QueryAsync<Credential>(query, new { CustomerID = 1 });
            //}
        }

        public void Edit(Credential entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Credential> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Credential> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Credential>> GetCredentialsByTypeAsync(int credentialTypeId)
        {
            string query = $"SELECT Id, UserId, CredentialTypeId, Identifier, Secret FROM Credentials WHERE CredentialTypeId = @credentialTypeId";
            var dbArgs = new DynamicParameters();
            dbArgs.Add("credentialTypeId", credentialTypeId);
            return await queryExecuter.GetManyAsync(query, dbArgs);
        }

        public void Remove(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
