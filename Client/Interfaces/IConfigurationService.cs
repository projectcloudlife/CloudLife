using Client.Models;
using System.Threading.Tasks;

namespace Client.Interfaces
{
    public interface IConfigurationService
    {
        Task<Configuration> GetAppConfiguration();
    }
}
