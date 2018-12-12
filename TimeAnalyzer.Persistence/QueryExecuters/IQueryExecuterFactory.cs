using System.Data.SqlClient;

namespace TimeAnalyzer.Persistence.QueryExecuters
{
    public interface ITransactableQueryExecuterFactory
    {
        IDapperQueryExecuter<T> GetDapperQueryExecuter<T>(SqlConnection connection, SqlTransaction sqlTransaction);
    }
}
