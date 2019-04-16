using ClientLogic.Interfaces;
using ClientLogic.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace Client.Services
{
    public class ConfigurationService : IConfigurationService
    {
        async public Task<Configuration> GetAppConfiguration()
        {
            var uri = new Uri("ms-appx:///Assets/configuration.json");
            var file = await StorageFile.GetFileFromApplicationUriAsync(uri);
            var fileString = await FileIO.ReadTextAsync(file);

            return JsonConvert.DeserializeObject<Configuration>("");
        }
    }
}
