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
        IUserRepository _repo;

        public AuthService(IUserRepository repo)
        {
            _repo = repo;
        }
        public async Task<LoginResponse> Login(AuthInfo authInfo)
        {
            LoginResponse res = new LoginResponse();
            UserDB user = new UserDB { AuthInfo = authInfo };
            using (_repo)
            {
                UserDB userDb = await _repo.Get(await GetId(authInfo));
                if (userDb == null) res.AuthInfo = AuthEnum.BadUsername;
                else if (userDb.AuthInfo.Password != authInfo.Password) res.AuthInfo = AuthEnum.BadPassword;
                else
                {
                    res.AuthInfo = AuthEnum.Success;
                    //add toekn
                }
                return res;
            }

        }

        private async Task<int> GetId(AuthInfo authInfo)
        {
            using (_repo)
            {
                var users = await _repo.GetWhere(u => u.AuthInfo.Username == authInfo.Username);
                var user = users.FirstOrDefault();
                return user.Id;
            }
        }

        private bool PasswordCheck(string password)
        {
            return password.Length > 5;
        }

        public async Task<AuthEnum> Register(AuthInfo authInfo)
        {
            AuthEnum res = new AuthEnum();
            if (PasswordCheck(authInfo.Password)) res = AuthEnum.BadPassword;
            else
            {
                UserDB user = new UserDB { AuthInfo = authInfo };
                using (_repo)
                {
                    try
                    {
                        await _repo.Create(user);
                        res = AuthEnum.Success;
                    }
                    catch
                    {
                        res = AuthEnum.BadUsername;
                    }
                }
            }
            return res;
        }
    }
}
