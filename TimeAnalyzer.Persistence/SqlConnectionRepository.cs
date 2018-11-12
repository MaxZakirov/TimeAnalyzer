using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace TimeAnalyzer.Persistence
{
    public abstract class SqlConnectionRepository
    {
        private readonly string connectionString;

        public SqlConnectionRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(connectionString);
            }
        }
    }
}
