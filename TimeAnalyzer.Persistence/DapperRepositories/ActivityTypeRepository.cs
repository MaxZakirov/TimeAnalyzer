using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TimeAnalyzer.Domain.Interfaces;
using TimeAnalyzer.Domain.Models;
using TimeAnalyzer.Persistence.QueryExecuters;

namespace TimeAnalyzer.Persistence.DapperRepositories
{
    public class ActivityTypeRepository : IActivityTypeRepository
    {
        private readonly IDapperQueryExecuter<ActivityType> queryExecuter;

        public ActivityTypeRepository(IDapperQueryExecuter<ActivityType> queryExecuter)
        {
            this.queryExecuter = queryExecuter;
        }

        public int Add(ActivityType entity)
        {
            try
            {
                string query = $"INSERT INTO ActivityTypes(Name, ColorValue) " +
                    $"VALUES (@name, @colorValue); SELECT CAST(SCOPE_IDENTITY() AS INT)";

                var dbArgs = new DynamicParameters();
                dbArgs.Add("name", entity.Name);
                dbArgs.Add("colorValue", entity.ColorValue);

                int newtimeReportId = queryExecuter.Connection.Query<int>(query, dbArgs).Single();
                return newtimeReportId;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ActivityType>> GetAll()
        {
            string query = $"SELECT Id, Name, ColorValue, ImportanceFactor FROM ActivityTypes";
            return await queryExecuter.GetManyAsync(query);
        }

        public Task<ActivityType> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int Id)
        {
            string query = $"DELETE FROM ActivityTypes WHERE Id=@id";
            var dbArgs = new DynamicParameters();
            dbArgs.Add("id", Id);
            queryExecuter.Execute(query, dbArgs);
        }

        public void Update(ActivityType entity)
        {
            string query = $"UPDATE ActivityTypes SET Name=@name, ColorValue=@color WHERE Id=@id";

            var dbArgs = new DynamicParameters();
            dbArgs.Add("id", entity.Id);
            dbArgs.Add("name", entity.Name);
            dbArgs.Add("color", entity.ColorValue);

            queryExecuter.Execute(query, dbArgs);
        }
    }
}
