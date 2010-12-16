using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DDD.Data.DB2V9.Tests
{
    /// <summary>
    /// This is a test class for DB2V9DataProviderTest and is intended
    /// to contain all DB2V9DataProviderTest Unit Tests
    /// </summary>
    [TestClass]
    public class DB2V9DataProviderTest
    {
        [TestMethod]
        public void Can_create_and_open_a_connection()
        {
            var provider = new DB2V9DataProvider();
            using (var connection = provider.CreateConnection())
            {
                connection.Open();
            }
        }
    }
}
