using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TimeAnalyzer.Domain.Interfaces;
using TimeAnalyzer.Domain.Models;
using TimeAnalyzer.Persistence.QueryExecuters;

namespace TimeAnalyzer.Persistence.DapperRepositories
{
    public class TimeReportRepository : ITimeReportRepository
    {
        private readonly IDapperQueryExecuter<TimeReport> queryExecuter;
        private readonly string getAllSQL = $"SELECT t.Id,t.Date,t.UserId,t.Duration,t.ActivityId,a.Id,a.IconPath,a.Name,a.ColorValue " +
                $"FROM TimeReports t JOIN Activities a ON a.Id = t.ActivityId WHERE t.UserId = @userId";

        public TimeReportRepository(
          IDapperQueryExecuter<TimeReport> queryExecuter)
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

        public void Update(TimeReport entity)
        {
            string query = $"UPDATE TimeReports SET Date=@date, Duration=@duration, ActivityId=@activityId WHERE Id=@id";

            var dbArgs = new DynamicParameters();
            dbArgs.Add("id", entity.Id);
            dbArgs.Add("date", entity.Date);
            dbArgs.Add("duration", entity.Duration);
            dbArgs.Add("activityId", entity.ActivityId);

            queryExecuter.Execute(query, dbArgs);
        }

        public Task<IEnumerable<TimeReport>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TimeReport>> GetAllUserReports(int userId)
        {
            var dbArgs = new DynamicParameters();
            dbArgs.Add("userId", userId);
            return await queryExecuter.Connection.QueryAsync<TimeReport, Activity, TimeReport>(getAllSQL,
                map: (tr, a) =>
                {
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

        public async Task<IEnumerable<TimeReport>> GetDayUserReports(int userId, DateTime date)
        {
            string query = getAllSQL + $" AND t.Date = @date";
            var dbArgs = new DynamicParameters();
            dbArgs.Add("userId", userId);
            dbArgs.Add("date", date.Date);
            return await queryExecuter.Connection.QueryAsync<TimeReport, Activity, TimeReport>(query,
                map: (tr, a) =>
                {
                    tr.Activity = a;
                    tr.ActivityId = a.Id;

                    return tr;
                },
                splitOn: "ActivityId",
                param: dbArgs);
        }

        public async Task<IEnumerable<TimeReport>> GetUserReportsInInterval(int userId, DateTime startDate, DateTime endDate)
        {
            string query = getAllSQL + $" and Date >= @startDate AND Date <= @endDate";
            var dbArgs = new DynamicParameters();
            dbArgs.Add("userId", userId);
            dbArgs.Add("startDate", startDate.Date);
            dbArgs.Add("endDate", endDate.Date);
            return await queryExecuter.Connection.QueryAsync<TimeReport, Activity, TimeReport>(query,
                map: (tr, a) =>
                {
                    tr.Activity = a;
                    tr.ActivityId = a.Id;

                    return tr;
                },
                splitOn: "ActivityId",
                param: dbArgs);
        }

        public Task<IEnumerable<TimeReport>> GetMonthUserReports(int id, byte monthNumber)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TimeReport>> GetYearUserReports(int id, short yearNumber)
        {
            throw new NotImplementedException();
        }

        public void Remove(int Id)
        {
            string query = $"DELETE FROM TimeReports WHERE Id=@id";
            var dbArgs = new DynamicParameters();
            dbArgs.Add("id", Id);
            queryExecuter.Execute(query, dbArgs);
        }
    }
}
