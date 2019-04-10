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

        public UserRepository(ContextDev context)
        {
            _context = context;
        }

        ContextDev _context;

        public async Task<UserDB> Create(UserDB user)
        {

            var usernameTake = _context.Users.Any(userDb => userDb.AuthInfo.Username == user.AuthInfo.Username);

            if (usernameTake)
            {
                throw new Exception("Username already exists.");
            }

            var newUserEntry = _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return newUserEntry.Entity;
        }

        public Task<UserDB> Get(int Id)
        {
            return Task.Run(() =>
            {
                return _context.Users.FirstOrDefault(userDb => userDb.Id == Id);
            });
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Task<IEnumerable<UserDB>> GetWhere(Func<UserDB, bool> expr)
        {
            return Task.Run(() =>
            {
                return (IEnumerable<UserDB>)_context.Users.Where(expr);
            });
        }
    }
}
