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
    public class PermissionsRepository : BaseRepository, IPermissionsRepository
    {

        public PermissionsRepository(CustomContext context) : base(context)
        {

        }

        public async Task CreateAsync(PermissionsModels Permissions)
        {
            await _context.permissions.AddAsync(Permissions);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PermissionsModels>> ReadAllAsync(string function_name)
        {
            var Permissions = from p in _context.permissions
                              select p;
            
            if(!string.IsNullOrEmpty(function_name))
                Permissions = Permissions.Where(f => f.function_names.function_name == function_name);

            Permissions = Permissions.Include(a => a.actions)
                                     .Include(f => f.function_names)
                                     .Include(c => c.create_user)
                                     .Include(u => u.update_user);

            return await Permissions.ToListAsync();
        }

        public async Task<PermissionsModels> ReadOneAsync(int id)
        {
            PermissionsModels Permissions = await _context.permissions
                .Include(a => a.actions)
                .Include(f => f.function_names)
                .Include(c => c.create_user)
                .Include(u => u.update_user)
                .SingleOrDefaultAsync(p => p.permission_id == id);

            return Permissions;
        }

        public async Task UpdateAsync(PermissionsModels Permissions)
        {
            _context.permissions.Update(Permissions);
            await _context.SaveChangesAsync();
        }

        public async Task<Boolean> DeleteAsync(int id, PermissionsModels Permissions)
        {
            _context.permissions.Remove(Permissions);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}