using ClientLogic.Interfaces;
using ClientLogic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace Client.Services
{
    public class ConfigurationService : IConfigurationService
    {
        async public Task<Configuration> GetAppConfiguration()
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri($"ms-appx:///Assets/configuration.json"));
            var fileString = await FileIO.ReadTextAsync(file);
            var hostConfig = JsonConvert.DeserializeObject<Configuration>(fileString);
            return hostConfig;
        }
    }
}
