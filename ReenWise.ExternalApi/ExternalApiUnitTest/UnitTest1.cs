using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReenWise.ExternalApi;

namespace ExternalApiUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CanGetDataFromApi()
        {
            var token = "123";
            var actual = Get.GetData(token);

            Assert.IsNotNull(actual);
        }
    }
}
