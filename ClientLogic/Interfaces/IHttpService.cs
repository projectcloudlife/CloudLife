using System.Threading.Tasks;

namespace ClientLogic.Interfaces
{
    public interface IHttpService
    {
        string JWTBearerToken { get; set; }
        Task<T> Get<T>(string path);
        Task<T> Post<T,TBody>(string path, TBody body);
        Task<T> Put<T,TBody>(string path, TBody body);
        Task<T> Delete<T>(string path);
    }
}
