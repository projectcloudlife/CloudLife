using Common.Enums;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DAL.Interfaces
{
 
    public interface IUserRepository
    {
        Task<AuthEnum> Login(AuthInfo authInfo);
        Task<AuthEnum> Register(AuthInfo authInfo);
    }
}
