using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeAnalyzer.Domain.Interfaces;
using TimeAnalyzer.Domain.Models.Users;
using System.Linq;
using System.Data.SqlClient;
using System.Data;

namespace TimeAnalyzer.Persistence.DapperRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDapperQueryExecuter<User> queryExecuter;
        private const int SqlDuplicateExceptionCode = 2627;

        public UserRepository(
            IDapperQueryExecuter<User> queryExecuter
            )
        {
            this.queryExecuter = queryExecuter;
        }

        public int Add(User entity)
        {
            try
            {
                string query = $"INSERT INTO Users(Name,Email,Password) VALUES (@name, @email, @password); SELECT CAST(SCOPE_IDENTITY() AS INT)";

                var dbArgs = new DynamicParameters();
                dbArgs.Add("name", entity.Name);
                dbArgs.Add("email", entity.Email);
                dbArgs.Add("password", entity.Password);

                int newUserId = queryExecuter.Connection.Query<int>(query, dbArgs).Single();
                return newUserId;
            }
            catch(SqlException ex)
            {
                if(ex.Number == SqlDuplicateExceptionCode)
                {
                    throw new DuplicateNameException($"The name {entity.Name} is already exists");
                }

                throw ex;
            }
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

        public async Task<User> GetByEmail(string email)
        {
            string query = $"SELECT Id, Name, Email, Password FROM Users WHERE Email = @email";
            var dbArgs = new DynamicParameters();
            dbArgs.Add("email", email);
            return await queryExecuter.GetAsync(query, dbArgs);
        }

        public void Remove(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByName(string name)
        {
            string query = $"SELECT Id, Name, Email, Password FROM Users WHERE Name = @name";
            var dbArgs = new DynamicParameters();
            dbArgs.Add("name", name);
            return await queryExecuter.GetAsync(query, dbArgs);
        }
    }
}
