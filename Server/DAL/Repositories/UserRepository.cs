using Microsoft.EntityFrameworkCore;
using Server.DAL.Interfaces;
using Server.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        bool disposed = false;

        public UserRepository(ContextDev context)
        {
            _context = context;
        }

        ContextDev _context;

        public async Task<UserDB> Create(UserDB user)
        {
            var usernameTake = _context.Users.Any(userDb => userDb.Username == user.Username);

            if (usernameTake)
            {
                throw new Exception("Username already exists.");
            }
            lock (_context)
            {
                var newUserEntry = _context.Users.Add(user);
                _context.SaveChanges();

                return newUserEntry.Entity;
            }
        }

        public Task<UserDB> Get(int Id)
        {
            return Task.Run(() =>
            {
                return _context.Users.Find(Id);
            });
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
        }

        public Task<IEnumerable<UserDB>> GetWhere(System.Linq.Expressions.Expression<Func<UserDB, bool>> expr)
        {
            return Task.Run(() =>
            {
                var result = (IEnumerable<UserDB>)_context.Users.AsNoTracking().Where(expr);
                return result;
            });
        }
    }
}
