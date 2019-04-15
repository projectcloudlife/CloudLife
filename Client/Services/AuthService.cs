using Client.Interfaces;
using Common.Enums;
using Common.Models;
using System.Threading.Tasks;

namespace Client.Services
{
    public class AuthService : IAuthService
    {

        public AuthService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        IHttpService _httpService;

        async public Task<LoginResponse> Login(AuthInfo authInfo)
        {
            var loginResponse = await _httpService.Post<LoginResponse,AuthInfo>("api/auth/login", authInfo);

            if(loginResponse.AuthResponse == AuthEnum.Success)
            {
                _httpService.JWTBearerToken = loginResponse.Token;
            }

            return loginResponse;
        }

        public void Logout()
        {
            _httpService.JWTBearerToken = "";
        }

        public Task<AuthEnum> Register(AuthInfo authInfo)
        {
            return _httpService.Post<AuthEnum, AuthInfo>("api/auth/register", authInfo);
        }

    }
}
