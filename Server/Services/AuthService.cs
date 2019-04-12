using Common.Enums;
using Common.Models;
using Server.DAL.Interfaces;
using Server.DAL.Models;
using Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services
{
    public class AuthService : IAuthService
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

            using (_userRepository)
            {
                var id = await GetId(authInfo);
                UserDB userDb = await _userRepository.Get(id);

                if (userDb == null)
                {
                    loginResponse.AuthInfo = AuthEnum.BadUsername;
                }
                else if (userDb.AuthInfo.Password != authInfo.Password)
                {
                    loginResponse.AuthInfo = AuthEnum.BadPassword;
                }
                else
                {
                    loginResponse.AuthInfo = AuthEnum.Success;
                    loginResponse.Token = await _tokenService.CreateToken();
                }
                return loginResponse;
            }
        }

        private async Task<int> GetId(AuthInfo authInfo)
        {
            using (_userRepository)
            {
                var users = await _userRepository.GetWhere(u => u.AuthInfo != null 
                && u.AuthInfo.Username == authInfo.Username);
                var user = users.FirstOrDefault();
                return user.Id;
            }
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
                UserDB user = new UserDB { AuthInfo = authInfo };
                using (_userRepository)
                {
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
            }
            return response;
        }
    }
}
