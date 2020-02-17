using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Context;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories.IRepositories;
using Repositories.Persistence;

namespace Repositories
{
    public class UsersRepository : BaseRepository, IUsersRepository
    {

        public UsersRepository(CustomContext context) : base(context)
        {

        }

        public async Task CreateAsync(UsersModels Users)
        {
            await _context.users.AddAsync(Users);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UsersModels>> ReadAllAsync(string account_number, string password, string user_name)
        {
            var Users = from u in _context.users
                        select u;

            if (!string.IsNullOrEmpty(account_number))
                Users.Where(a => a.account_number == account_number);

            if (!string.IsNullOrEmpty(password))
                Users.Where(p => p.password == password);

            if (!string.IsNullOrEmpty(user_name))
                Users.Where(u => u.user_name == user_name);

            Users = Users.Include(r => r.roles);

            return await Users.ToListAsync();
        }

        public async Task<UsersModels> ReadOneAsync(string id)
        {
            UsersModels Users = await _context.users
                                              .Include(r => r.roles)
                                              .SingleOrDefaultAsync(u => u.user_id == id);

            return Users;
        }

        public async Task<UsersModels> ReadOneAsync(string account_number, string password)
        {
            var Users = await _context.users
                                      .Include(r => r.roles)
                                      .SingleOrDefaultAsync(u => u.account_number == account_number && u.password == password);

            return Users;
        }

        public async Task UpdateAsync(UsersModels Users)
        {
            _context.users.Update(Users);
            await _context.SaveChangesAsync();
        }

        public async Task<Boolean> DeleteAsync(UsersModels Users)
        {
            _context.users.Remove(Users);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}