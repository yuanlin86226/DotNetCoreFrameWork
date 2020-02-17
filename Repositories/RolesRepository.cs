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
    public class RolesRepository : BaseRepository, IRolesRepository
    {

        public RolesRepository(CustomContext context) : base(context)
        {

        }

        public async Task CreateAsync(RolesModels Roles)
        {
            await _context.roles.AddAsync(Roles);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RolesModels>> ReadAllAsync(string role)
        {
            var Roles = from r in _context.roles
                        select r;

            if (!string.IsNullOrEmpty(role))
                Roles = Roles.Where(r => r.role == role);

            Roles = Roles.Include(u => u.create_user)
                         .Include(u => u.update_user);

            return await Roles.ToListAsync();
        }


        public async Task<RolesModels> ReadOneAsync(int id)
        {
            RolesModels Roles = await _context.roles
                                              .Include(u => u.create_user)
                                              .Include(u => u.update_user)
                                              .SingleOrDefaultAsync(x => x.role_id == id);

            return Roles;
        }

        public async Task UpdateAsync(RolesModels Roles)
        {
            _context.roles.Update(Roles);
            await _context.SaveChangesAsync();
        }

        public async Task<Boolean> DeleteAsync(RolesModels roles)
        {
            _context.roles.Remove(roles);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}