﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeAnalyzer.Domain.Interfaces
{
    public interface IRepository<T> 
        where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(int Id);

        void Remove(int Id);

        int Add(T entity);

        void Update(T entity);
    }
}
