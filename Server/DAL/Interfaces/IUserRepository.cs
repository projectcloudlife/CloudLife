﻿using Common.Enums;
using Common.Models;
using Server.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DAL.Interfaces
{
 
    public interface IUserRepository:IDisposable
    {
        //  returns the created UserDB, if username taken throws exception.
        Task<UserDB> Create(UserDB user);

        //  returns UserDB, if not found returns null.
        Task<UserDB> Get(int Id);

        Task<IEnumerable<UserDB>> GetWhere(System.Linq.Expressions.Expression<Func<UserDB, bool>> expr);       

    }
}
