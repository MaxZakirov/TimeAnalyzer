using System.Data.SqlClient;

namespace TimeAnalyzer.Persistence.QueryExecuters
{
    public class TransactableQueryExecuterFactory : ITransactableQueryExecuterFactory
    {
        public IDapperQueryExecuter<T> GetDapperQueryExecuter<T>(SqlConnection connection, SqlTransaction sqlTransaction)
        {
            return new TransactableDapperQueryExecuter<T>(connection, sqlTransaction);
        }
    }
}
