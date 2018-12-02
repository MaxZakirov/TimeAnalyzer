using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeAnalyzer.Persistence
{
    public class DapperQueryExecuter<T> : IDapperQueryExecuter<T>
    {
        private readonly string connectionString;

        public DapperQueryExecuter(IConfiguration config)
        {
            this.connectionString = config.GetConnectionString("mainDBConnectionString");
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(connectionString);
            }
        }

        public void Execute(string query, object param)
        {
            throw new NotImplementedException();
        }

        public void ExecuteWithOut(string query, object param)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetAsync(string query, object param)
        {
            using (IDbConnection connection = this.Connection)
            {
                connection.Open();
                var res = await connection.QueryAsync<T>(query, param);
                return res.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<T>> GetManyAsync(string query, object param = null)
        {
            using (IDbConnection connection = this.Connection)
            {
                connection.Open();
                var res = await connection.QueryAsync<T>(query, param);
                return res;
            }
        }
    }
}
