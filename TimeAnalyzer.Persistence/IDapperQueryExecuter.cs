using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeAnalyzer.Persistence
{
    public interface IDapperQueryExecuter<T>
    {
        Task<T> GetAsync(string query, object param);

        Task<IEnumerable<T>> GetManyAsync(string query, object param);

        void Execute(string query, object param);

        void ExecuteWithOut(string query, object param);
    }
}
