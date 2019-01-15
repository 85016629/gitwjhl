using fx.Domain.core;
using fx.Infra.Data.MongDb;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;

namespace CustomerTestProj
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var eventStore = new EventStore();

            var e = new DomainEvent()
            {
                AggregateRootType = "Test",
                EventData = "sdfsdfsdf"
            };

            eventStore.SaveEvent(e);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var eventStore = new EventStore();
            var filter = Builders<DomainEvent>.Filter.Eq("_id", "5c3d8337e3fb85d40592108e");
            var domain = eventStore.GetAll().Count(filter);
            Assert.IsTrue(domain != 0);
        }
    }
}
