using System.Data;
using System.Data.SqlClient;
using DDD.Data.Storage;

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
