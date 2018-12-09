using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeAnalyzer.Domain.Interfaces;
using TimeAnalyzer.Domain.Models;

namespace TimeAnalyzer.Persistence.DapperRepositories
{
    public class TimeReportRepository : ITimeReportRepository
    {
        private readonly IDapperQueryExecuter<TimeReport> queryExecuter;

        public TimeReportRepository(
          IDapperQueryExecuter<TimeReport> queryExecuter
          )
        {
            this.queryExecuter = queryExecuter;
        }

        public int Add(TimeReport entity)
        {
            try
            {
                string query = $"INSERT INTO TimeReports(Date,Duration,ActivityId, UserId) " +
                    $"VALUES (@date, @duration, @activity, @userId); SELECT CAST(SCOPE_IDENTITY() AS INT)";

                var dbArgs = new DynamicParameters();
                dbArgs.Add("date", entity.Date);
                dbArgs.Add("duration", entity.Duration);
                dbArgs.Add("activity", entity.ActivityId);
                dbArgs.Add("userId", entity.UserId);

                int newtimeReportId = queryExecuter.Connection.Query<int>(query, dbArgs).Single();
                return newtimeReportId;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public void Edit(TimeReport entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TimeReport>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TimeReport>> GetAllUserReports(int userId)
        {
            string query = $"SELECT t.Id,t.Date,t.UserId,t.Duration,t.ActivityId,a.Id,a.IconPath,a.Name " +
                $"FROM TimeReports t JOIN Activities a ON a.Id = t.ActivityId WHERE t.UserId = @userId";
            var dbArgs = new DynamicParameters();
            dbArgs.Add("userId", userId);
            return await queryExecuter.Connection.QueryAsync<TimeReport,Activity,TimeReport>(query,
                map: (tr,a) => {
                    tr.Activity = a;

                    return tr;
                },
                splitOn: "ActivityId",
                param: dbArgs);
        }

        public Task<TimeReport> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TimeReport> GetDayUserReports(int userId, DateTime date)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TimeReport> GetInterimUserReports(int id, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TimeReport> GetMonthUserReports(int id, byte monthNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TimeReport> GetYearUserReports(int id, short yearNumber)
        {
            throw new NotImplementedException();
        }

        public void Remove(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
