using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DDD.Data.SqlServer.Tests
{
    /// <summary>
    /// This is a test class for SqlServerDataProviderTest and is intended
    /// to contain all SqlServerDataProviderTest Unit Tests
    /// </summary>
    [TestClass]
    public class SqlServerDataProviderTest
    {
        private readonly string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=DataFramework_Test;Integrated Security=True";

        [TestMethod]
        public void CanCreateAConnection()
        {
            var provider = new SqlServerDataProvider(connectionString);
            using (var connection = provider.CreateConnection())
            {
                connection.Open();
            }
        }
    }
}
