using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDD.Data.Storage;
using System.Data;
using System.Data.SqlClient;

namespace DDD.Data.SqlServer
{
    public class SqlServerDataProvider : IDataProvider
    {
        private readonly string connectionString;

        public SqlServerDataProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
