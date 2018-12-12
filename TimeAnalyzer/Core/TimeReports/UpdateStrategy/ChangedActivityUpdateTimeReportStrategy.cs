using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using TimeAnalyzer.Domain.Interfaces;
using TimeAnalyzer.Domain.Models;

namespace TimeAnalyzer.Core.TimeReports.UpdateStrategy
{
    public class ChangedActivityUpdateTimeReportStrategy : TimeReportUpdateStrategy
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly TimeReport oldActivityTimeReport;

        public ChangedActivityUpdateTimeReportStrategy(
            IUnitOfWork unitOfWork,
            IEnumerable<TimeReport> dateTimeReports,
            TimeReport newTimeReport,
            TimeReport oldActivityTimeReport)
            : base(dateTimeReports,newTimeReport)
        {
            this.unitOfWork = unitOfWork;
            this.oldActivityTimeReport = oldActivityTimeReport;
        }

        public override void PrepareDateTimeReportsToValidation()
        {
            oldActivityTimeReport.Duration += newTimeReport.Duration;
        }

        public override void UpdateTimeReportDataInRepository()
        {
            using (unitOfWork)
            {
                try
                {
                    unitOfWork.OpenTransaction();
                    unitOfWork.TimeReports.Remove(newTimeReport.Id);
                    unitOfWork.TimeReports.Update(oldActivityTimeReport);
                }
                catch(Exception ex)
                {
                    unitOfWork.Rollback();
                }
            }
        }
    }
}
