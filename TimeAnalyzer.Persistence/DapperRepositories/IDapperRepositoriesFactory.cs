using TimeAnalyzer.Domain.Interfaces;
using TimeAnalyzer.Domain.Models;
using TimeAnalyzer.Domain.Models.Users;
using TimeAnalyzer.Persistence.QueryExecuters;

namespace TimeAnalyzer.Persistence.DapperRepositories
{
    public interface IDapperRepositoriesFactory
    {
        IActivityRepository CreateActivityRepository(IDapperQueryExecuter<Activity> dapperQueryExecuter);

        ITimeReportRepository CreateTimeReportRepository(IDapperQueryExecuter<TimeReport> dapperQueryExecuter);

        IUserRepository CreateUserRepository(IDapperQueryExecuter<User> dapperQueryExecuter);
    }
}
