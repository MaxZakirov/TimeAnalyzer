using System;
using System.Collections.Generic;
using TimeAnalyzer.Domain.Models;

namespace TimeAnalyzer.Domain.Interfaces
{
    public interface ITimeReportsRepository : IRepository<TimeReport>
    {
        IEnumerable<TimeReport> GetAllUserReports(int userId);

        IEnumerable<TimeReport> GetDayUserReports(int userId, DateTime date);

        IEnumerable<TimeReport> GetInterimUserReports(int id, DateTime startDate, DateTime endDate);

        IEnumerable<TimeReport> GetMonthUserReports(int id, byte monthNumber);

        IEnumerable<TimeReport> GetYearUserReports(int id, short yearNumber);
    }
}
