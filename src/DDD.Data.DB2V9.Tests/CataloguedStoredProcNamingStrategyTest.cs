using DDD.Data.DB2V9;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DDD.Data.Storage;
using System;

namespace DDD.Data.DB2V9.Tests
{
    /// <summary>
    /// This is a test class for CataloguedStoredProcNamingStrategyTest and is intended
    /// to contain all CataloguedStoredProcNamingStrategyTest Unit Tests
    /// </summary>
    [TestClass()]
    public class CataloguedStoredProcNamingStrategyTest
    {
        public class MyModel { }

        private CataloguedStoredProcNamingStrategy strategy;

        [TestInitialize]
        public void Setup()
        {
            this.strategy = new CataloguedStoredProcNamingStrategy();
        }

        [TestMethod]
        public void Can_get_a_stored_proc_name()
        {
            string spName = "foobar";
            this.strategy.SetStoredProcName<GetByIdAction<MyModel>>(spName);
            Assert.AreEqual(spName, this.strategy.GetStoredProcNameFor<GetByIdAction<MyModel>>());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void It_should_throw_if_the_action_is_not_registered()
        {
            this.strategy.GetStoredProcNameFor<GetByIdAction<MyModel>>();
        }
    }
}
