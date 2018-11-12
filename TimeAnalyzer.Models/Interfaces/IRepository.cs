using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeAnalyzer.Domain.Interfaces
{
    public interface IRepository<T> 
        where T : class
    {
        IEnumerable<T> GetAll();

        Task<T> GetById(int Id);

        void Remove(int Id);

        Task<int> AddAssync(T entity);

        void Edit(T entity);
    }
}
