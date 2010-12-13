using System.Data;
using System.Linq;
using DDD.Data.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DDD.Data.MetaData.Tests
{
    /// <summary>
    /// This is a test class for MetaDataStoreTest and is intended
    /// to contain all MetaDataStoreTest Unit Tests
    /// </summary>
    [TestClass]
    public class MetaDataStoreTest
    {
        private MetaDataStore store;

        [TestInitialize]
        public void Given()
        {
            store = new MetaDataStore();
            store.BuildMetaDataFor(this.GetType().Assembly);
        }

        [TestMethod]
        public void It_should_contains_TableInfo_for_all_models()
        {
            Assert.IsNotNull(store.GetTableInfoFor<Customer>());
            Assert.IsNotNull(store.GetTableInfoFor<Order>());
            Assert.IsNotNull(store.GetTableInfoFor<Product>());
        }

        [TestMethod]
        public void It_should_have_a_PrimaryKey_for_each_TableInfo()
        {
            Assert.IsNotNull(store.GetTableInfoFor<Customer>().PrimaryKey);
            Assert.IsNotNull(store.GetTableInfoFor<Order>().PrimaryKey);
            Assert.IsNotNull(store.GetTableInfoFor<Product>().PrimaryKey);
        }

        [TestMethod]
        public void Then_non_named_column_should_have_been_given_the_name_of_the_property()
        {
            var tableInfo = store.GetTableInfoFor<Customer>();
            Assert.AreEqual("Id", tableInfo.PrimaryKey.Name);
            Assert.IsNotNull(tableInfo.Columns.FirstOrDefault(_ => _.Name == "LastName"));
            Assert.IsNotNull(tableInfo.References.FirstOrDefault(_ => _.Name == "AddressId"));
        }

        [TestMethod]
        public void It_should_have_reference_info_for_Order()
        {
            Assert.IsTrue(store.GetTableInfoFor<Order>().References.Count() > 0);
            var referenceInfo = store.GetTableInfoFor<Order>().References.FirstOrDefault(_ => _.ReferenceType == typeof(Customer));
            Assert.IsNotNull(referenceInfo);
            Assert.AreEqual("CustomerId", referenceInfo.Name);
            Assert.AreEqual(DbType.Int32, referenceInfo.DbType);
        }
    }
}
