using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace TimeAnalyzer.Persistence
{
    public interface IDapperQueryExecuter<T>
    {
        IDbConnection Connection { get; }

        Task<T> GetAsync(string query, object param);

        Task<IEnumerable<T>> GetManyAsync(string query, object param = null);

        void Execute(string query, object param);

        void ExecuteWithOut(string query, object param);
    }
}
