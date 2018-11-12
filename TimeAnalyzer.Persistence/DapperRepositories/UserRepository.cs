using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeAnalyzer.Domain.Interfaces;
using TimeAnalyzer.Domain.Models.Users;

namespace TimeAnalyzer.Persistence.DapperRepositories
{
    public class UserRepository : IUsersRepository
    {
        private readonly IDapperQueryExecuter<User> queryExecuter;

        public UserRepository(
            IDapperQueryExecuter<User> queryExecuter
            )
        {
            this.queryExecuter = queryExecuter;
        }

        public Task<int> AddAssync(User entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(User entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetById(int Id)
        {   
            string query = $"SELECT Id, Name, Email, Password FROM Users WHERE Id = @id";
            var dbArgs = new DynamicParameters();
            dbArgs.Add("id", Id);
            return await queryExecuter.GetAsync(query, dbArgs);
        }

        public async Task<User> GetByEmail(string name)
        {
            string query = $"SELECT Id, Name, Email, Password FROM Users WHERE Email = @Email";
            var dbArgs = new DynamicParameters();
            dbArgs.Add("name", name);
            return await queryExecuter.GetAsync(query, dbArgs);
        }

        public void Remove(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
