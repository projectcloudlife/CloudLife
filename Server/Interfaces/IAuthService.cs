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
        LoginResponse Login(AuthInfo authInfo);
        AuthEnum Register(AuthInfo authInfo);
    }
}
