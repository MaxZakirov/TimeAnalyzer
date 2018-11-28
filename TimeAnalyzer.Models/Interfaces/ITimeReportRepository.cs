using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeAnalyzer.Domain.Models;

namespace TimeAnalyzer.Domain.Interfaces
{
    public interface ITimeReportRepository : IRepository<TimeReport>
    {
        Task<IEnumerable<TimeReport>> GetAllUserReports(int userId);

        IEnumerable<TimeReport> GetDayUserReports(int userId, DateTime date);

        IEnumerable<TimeReport> GetInterimUserReports(int id, DateTime startDate, DateTime endDate);

        IEnumerable<TimeReport> GetMonthUserReports(int id, byte monthNumber);

        IEnumerable<TimeReport> GetYearUserReports(int id, short yearNumber);
    }
}
