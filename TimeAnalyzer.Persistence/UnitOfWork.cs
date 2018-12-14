using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using TimeAnalyzer.Domain.Interfaces;
using TimeAnalyzer.Domain.Models;
using TimeAnalyzer.Domain.Models.Users;
using TimeAnalyzer.Persistence.DapperRepositories;
using TimeAnalyzer.Persistence.QueryExecuters;

namespace TimeAnalyzer.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly string connectionString;
        private readonly ITransactableQueryExecuterFactory queryExecuterFactory;
        private readonly IDapperRepositoriesFactory repositoriesFactory;

        private IActivityRepository activityRepository;
        private ITimeReportRepository timeReportRepository;
        private IUserRepository userRepository;

        private SqlTransaction sqlTransaction;
        private bool transactionOpened = false;
        private SqlConnection sqlConnection;


        public UnitOfWork(
            IConfiguration configuration,
            ITransactableQueryExecuterFactory queryExecuterFactory,
            IDapperRepositoriesFactory repositoriesFactory
            )
        {
            connectionString = configuration.GetConnectionString("mainDBConnectionString");
            this.queryExecuterFactory = queryExecuterFactory;
            this.repositoriesFactory = repositoriesFactory;
        }

        public IActivityRepository Activities
        {
            get
            {
                if (activityRepository == null)
                {
                    activityRepository = LoadRepository<Activity>(repositoriesFactory.CreateActivityRepository) as IActivityRepository;
                }

                return activityRepository;
            }
        }

        public ITimeReportRepository TimeReports
        {
            get
            {
                if (activityRepository == null)
                {
                    timeReportRepository = LoadRepository<TimeReport>(repositoriesFactory.CreateTimeReportRepository) as ITimeReportRepository;
                }

                return timeReportRepository;
            }
        }

        public IUserRepository Users
        {
            get
            {
                if (activityRepository == null)
                {
                    userRepository = LoadRepository<User>(repositoriesFactory.CreateUserRepository) as IUserRepository;
                }

                return userRepository;
            }
        }

        public void CommitTransaction()
        {
            transactionOpened = false;
            sqlTransaction.Commit();
        }

        public void Dispose()
        {
            if (transactionOpened)
                CommitTransaction();

            sqlTransaction?.Dispose();
            sqlConnection?.Dispose();
            ResetAllRepositories();
        }

        public void OpenTransaction()
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            sqlTransaction = sqlConnection.BeginTransaction();
            transactionOpened = true;
        }

        public void Rollback()
        {
            transactionOpened = false;
            sqlTransaction.Rollback();
        }

        private void ResetAllRepositories()
        {
            activityRepository = null;
            timeReportRepository = null;
            userRepository = null;
        }

        private IRepository<T> LoadRepository<T>(Func<IDapperQueryExecuter<T>, IRepository<T>> factoryMethod)
            where T : class
        {
            return factoryMethod.Invoke(queryExecuterFactory.GetDapperQueryExecuter<T>(sqlConnection, sqlTransaction));
        }
    }
}
