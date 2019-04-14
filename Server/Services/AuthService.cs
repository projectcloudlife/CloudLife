using Common.Enums;
using Common.Models;
using Server.DAL.Interfaces;
using Server.DAL.Models;
using Server.Extantions;
using Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services
{
    public class AuthService : IAuthService, IDisposable
    {
        IUserRepository _userRepository;
        ITokenGeneratorService _tokenService;

        public AuthService(IUserRepository repo, ITokenGeneratorService tokenServ)
        {
            _userRepository = repo;
            _tokenService = tokenServ;
        }

        public async Task<LoginResponse> Login(AuthInfo authInfo)
        {
            LoginResponse loginResponse = new LoginResponse();
            var id = await _userRepository.GetId(authInfo);

            UserDB userDb = await _userRepository.Get(id);

            if (userDb == null)
            {
                loginResponse.AuthResponse = AuthEnum.BadUsername;
            }
            else if (userDb.Password != authInfo.Password)
            {
                loginResponse.AuthResponse = AuthEnum.BadPassword;
            }
            else
            {
                loginResponse.AuthResponse = AuthEnum.Success;
                loginResponse.Token = await _tokenService.CreateToken(userDb.Id);
            }

            return loginResponse;

        }

        private bool IsPasswordValid(string password)
        {
            return password.Length > 5;
        }

        public async Task<AuthEnum> Register(AuthInfo authInfo)
        {
            AuthEnum response = new AuthEnum();

            if (!IsPasswordValid(authInfo.Password))
            {
                response = AuthEnum.BadPassword;
            }
            else
            {
                UserDB user = new UserDB { Username = authInfo.Username, Password = authInfo.Password };
                    try
                    {
                        await _userRepository.Create(user);
                        response = AuthEnum.Success;
                    }
                    catch
                    {
                        response = AuthEnum.BadUsername;
                    }
            }
            return response;
        }

        public void Dispose()
        {
            _userRepository.Dispose();
        }
    }
}
