using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TimeAnalyzer.Domain.Interfaces;
using TimeAnalyzer.Domain.Models;
using TimeAnalyzer.Persistence.QueryExecuters;

namespace TimeAnalyzer.Persistence.DapperRepositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly string getAllQuery = "SELECT Id, Name, TypeId FROM Activities";
        private readonly IDapperQueryExecuter<Activity> queryExecuter;

        public ActivityRepository(IDapperQueryExecuter<Activity> queryExecuter)
        {
            this.queryExecuter = queryExecuter;
        }

        public int Add(Activity entity)
        {
            try
            {
                string query = $"INSERT INTO Activities(Name,ActivityTypeId) " +
                    $"VALUES (@name, @typeId); SELECT CAST(SCOPE_IDENTITY() AS INT)";

                var dbArgs = new DynamicParameters();
                dbArgs.Add("name", entity.Name);
                dbArgs.Add("typeId", entity.TypeId);

                int newtimeReportId = queryExecuter.Connection.Query<int>(query, dbArgs).Single();
                return newtimeReportId;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public void Update(Activity entity)
        {
            string query = $"UPDATE Activities SET Name=@name, ActivityTypeId=@typeId WHERE Id=@id";

            var dbArgs = new DynamicParameters();
            dbArgs.Add("id", entity.Id);
            dbArgs.Add("name", entity.Name);
            dbArgs.Add("typeId", entity.TypeId);

            queryExecuter.Execute(query, dbArgs);
        }

        public async Task<IEnumerable<Activity>> GetAll()
        {
            string query = $"{getAllQuery}";
            return await queryExecuter.GetManyAsync(query);
        }

        public void Remove(int Id)
        {
            string query = $"DELETE FROM Activities WHERE Id=@id";
            var dbArgs = new DynamicParameters();
            dbArgs.Add("id", Id);
            queryExecuter.Execute(query, dbArgs);
        }

        public async Task<Activity> GetById(int Id)
        {
            string query = $"{getAllQuery} WHERE Id = @id";
            var dbArgs = new DynamicParameters();
            dbArgs.Add("id", Id);
            return await queryExecuter.GetAsync(query, dbArgs);
        }
    }
}
