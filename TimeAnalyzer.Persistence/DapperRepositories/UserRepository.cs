using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeAnalyzer.Domain.Interfaces;
using TimeAnalyzer.Domain.Models.Users;
using System.Linq;

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

        public int Add(User entity)
        {
            string query = $"INSERT INTO Users(Name,Email,Password) VALUES (@name, @email, @password); SELECT CAST(SCOPE_IDENTITY() AS INT)";

            var dbArgs = new DynamicParameters();
            dbArgs.Add("name", entity.Name);
            dbArgs.Add("email", entity.Email);
            dbArgs.Add("password", entity.Password);

            int newUserId = queryExecuter.Connection.Query<int>(query, dbArgs).Single();
            return newUserId;
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
            string query = $"SELECT Id, Name, Email, Password FROM Users WHERE Email = @email";
            var dbArgs = new DynamicParameters();
            dbArgs.Add("email", name);
            return await queryExecuter.GetAsync(query, dbArgs);
        }

        public void Remove(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
