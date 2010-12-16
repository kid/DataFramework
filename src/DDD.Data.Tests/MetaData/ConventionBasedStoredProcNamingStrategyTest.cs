using DDD.Data.MetaData;
using DDD.Data.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DDD.Data.Tests
{
    /// <summary>
    /// This is a test class for ConventionBasedStoredProcNamingStrategyTest and is intended
    /// to contain all ConventionBasedStoredProcNamingStrategyTest Unit Tests
    /// </summary>
    [TestClass]
    public class ConventionBasedStoredProcNamingStrategyTest
    {
        public class MyModel { }

        private ConventionBasedStoredProcNamingStrategy strategy;

        [TestInitialize]
        public void Setup()
        {
            strategy = new ConventionBasedStoredProcNamingStrategy();
        }

        [TestMethod()]
        public void GetStoredProcNameForTest()
        {
            Assert.AreEqual("MyModel_Insert", strategy.GetStoredProcNameFor<InsertAction<MyModel>>());
            Assert.AreEqual("MyModel_Update", strategy.GetStoredProcNameFor<UpdateAction<MyModel>>());
            Assert.AreEqual("MyModel_Delete", strategy.GetStoredProcNameFor<DeleteAction<MyModel>>());
            Assert.AreEqual("MyModel_GetById", strategy.GetStoredProcNameFor<GetByIdAction<MyModel>>());
        }
    }
}
