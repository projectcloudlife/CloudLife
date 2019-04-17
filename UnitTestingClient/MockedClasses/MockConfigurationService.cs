using ClientLogic.Interfaces;
using ClientLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestingClient.MockedClasses
{
    public class MockConfigurationService : IConfigurationService
    {
        public Task<Configuration> GetAppConfiguration()
        {
            return Task.Run(() =>
            {
                return new Configuration()
                {
                    Host = "https://localhost:44390"
                };
            }); 
        }
    }
}
