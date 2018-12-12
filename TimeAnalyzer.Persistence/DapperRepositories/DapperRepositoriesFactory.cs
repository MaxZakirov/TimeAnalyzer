using TimeAnalyzer.Domain.Interfaces;
using TimeAnalyzer.Domain.Models;
using TimeAnalyzer.Domain.Models.Users;
using TimeAnalyzer.Persistence.QueryExecuters;

namespace TimeAnalyzer.Persistence.DapperRepositories
{
    public class DapperRepositoriesFactory : IDapperRepositoriesFactory
    {
        public IActivityRepository CreateActivityRepository(IDapperQueryExecuter<Activity> dapperQueryExecuter)
        {
            return new ActivityRepository(dapperQueryExecuter);
        }

        public ITimeReportRepository CreateTimeReportRepository(IDapperQueryExecuter<TimeReport> dapperQueryExecuter)
        {
            return new TimeReportRepository(dapperQueryExecuter);
        }

        public IUserRepository CreateUserRepository(IDapperQueryExecuter<User> dapperQueryExecuter)
        {
            return new UserRepository(dapperQueryExecuter);
        }
    }
}
