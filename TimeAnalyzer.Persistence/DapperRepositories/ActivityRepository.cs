using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeAnalyzer.Domain.Interfaces;
using TimeAnalyzer.Domain.Models;
using TimeAnalyzer.Persistence.QueryExecuters;

namespace TimeAnalyzer.Persistence.DapperRepositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly IDapperQueryExecuter<Activity> queryExecuter;

        public ActivityRepository(IDapperQueryExecuter<Activity> queryExecuter)
        {
            this.queryExecuter = queryExecuter;
        }

        public int Add(Activity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Activity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Activity>> GetAll()
        {
            string query = $"SELECT Id, Name, IconPath FROM Activities";
            return await queryExecuter.GetManyAsync(query);
        }

        public void Remove(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<Activity> GetById(int Id)
        {
            string query = $"SELECT Id, Name, IconPath FROM Activities WHERE Id = @id";
            var dbArgs = new DynamicParameters();
            dbArgs.Add("id", Id);
            return await queryExecuter.GetAsync(query, dbArgs);
        }
    }
}
