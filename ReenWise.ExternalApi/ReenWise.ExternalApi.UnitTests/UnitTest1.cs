using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReenWise.ExternalApi.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var actual = EventLogCreator.CreateEventLog("TestSource", "TestLog", "TestService");
            Assert.IsNotNull(actual);
        }
    }
}
