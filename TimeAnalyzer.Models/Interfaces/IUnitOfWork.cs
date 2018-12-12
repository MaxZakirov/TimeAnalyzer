using System;
using System.Collections.Generic;
using System.Text;

namespace TimeAnalyzer.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IActivityRepository Activities { get; }

        ITimeReportRepository TimeReports { get; }

        IUserRepository Users { get; }

        void OpenTransaction();

        void CommitTransaction();

        void Rollback();
    }
}
