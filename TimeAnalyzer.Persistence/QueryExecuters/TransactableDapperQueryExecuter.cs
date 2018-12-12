using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TimeAnalyzer.Persistence.QueryExecuters
{
    class TransactableDapperQueryExecuter<T> : IDapperQueryExecuter<T>
    {
        private IDbConnection connection;
        private readonly SqlTransaction transaction;

        public IDbConnection Connection => connection;

        public TransactableDapperQueryExecuter(SqlConnection connection, SqlTransaction transaction)
        {
            this.connection = connection;
            this.transaction = transaction;
        }

        public void Dispose()
        {
            connection.Dispose();
        }

        public void Execute(string query, object param)
        {
            connection.Execute(query, param, transaction);
        }

        public void ExecuteWithOut(string query, object param)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetAsync(string query, object param)
        {
            return (await connection.QueryAsync<T>(query, param, transaction)).FirstOrDefault();
        }

        public async Task<IEnumerable<T>> GetManyAsync(string query, object param = null)
        {
            return await connection.QueryAsync<T>(query, param, transaction);
        }
    }
}
