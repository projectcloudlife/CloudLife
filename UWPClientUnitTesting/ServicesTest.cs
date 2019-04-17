
using System;
using Client.Services;
using ClientLogic.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UWPClientUnitTesting
{
    [TestClass]
    public class ServicesTest
    {
        [TestMethod]
        public void ICofigurationServiceTest()
        {
            IConfigurationService service = new ConfigurationService();

            var config =  service.GetAppConfiguration().Result;

            Assert.AreEqual(config.Host, "https://localhost:44390");

        }
    }
}
