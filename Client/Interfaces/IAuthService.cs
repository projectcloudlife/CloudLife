using Common.Enums;
using Common.Models;
using System.Threading.Tasks;

namespace Client.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(AuthInfo authInfo);
        Task<AuthEnum> Register(AuthInfo authInfo);
        void Logout(); 
    }
}
