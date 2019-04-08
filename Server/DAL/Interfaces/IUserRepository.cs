using Common.Enums;
using Common.Models;
using Server.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DAL.Interfaces
{
 
    public interface IUserRepository
    {
        Task<UserDB> Get(int Id);
        Task<UserDB> Create(UserDB user);
    }
}
