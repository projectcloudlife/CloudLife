using ClientLogic.Models;
using System.Threading.Tasks;

namespace ClientLogic.Interfaces
{
    public interface IConfigurationService
    {
        Task<Configuration> GetAppConfiguration();
    }
}
