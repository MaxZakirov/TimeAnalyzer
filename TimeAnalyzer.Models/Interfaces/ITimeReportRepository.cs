using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TimeAnalyzer.Domain.Models;

namespace TimeAnalyzer.Domain.Interfaces
{
    public interface ITimeReportRepository : IRepository<TimeReport>
    {
        Task<IEnumerable<TimeReport>> GetAllUserReports(int userId);

        Task<IEnumerable<TimeReport>> GetDayUserReports(int userId, DateTime date);

        Task<IEnumerable<TimeReport>> GetUserReportsInInterval(int userId, DateTime startDate, DateTime endDate);

        Task<IEnumerable<TimeReport>> GetMonthUserReports(int id, byte monthNumber);

        Task<IEnumerable<TimeReport>> GetYearUserReports(int id, short yearNumber);
    }
}
