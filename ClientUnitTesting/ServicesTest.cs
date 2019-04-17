using Client.Services;
using ClientLogic.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClientUnitTesting
{
    [TestClass]
    public class ServicesTest
    {
        [TestMethod]
        public void IConfigurationServiceTest()
        {
            IConfigurationService configurationService = new ConfigurationService();
            var config = configurationService.GetAppConfiguration();
        }
    }
}
