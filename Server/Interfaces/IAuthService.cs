using Common.Enums;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(AuthInfo authInfo);
        Task<AuthEnum> Register(AuthInfo authInfo);
        Task<int> GetId(AuthInfo authInfo);
    }
}
