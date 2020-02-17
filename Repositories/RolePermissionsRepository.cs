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
    public class RolePermissionsRepository : BaseRepository, IRolePermissionsRepository
    {

        public RolePermissionsRepository(CustomContext context) : base(context)
        {

        }

        public async Task CreateAsync(List<RolePermissionsModels> RolePermissions)
        {
            await _context.role_permissions.AddRangeAsync(RolePermissions);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RolePermissionsModels>> ReadAllAsync(string role)
        {
            var RolePermissions = from r in _context.role_permissions
                                  select r;

            if (!string.IsNullOrEmpty(role))
                RolePermissions = RolePermissions.Where(r => r.roles.role == role);

            RolePermissions = RolePermissions.Include(r => r.roles)
                                             .Include(p => p.permissions)
                                                 .ThenInclude(permissions => permissions.function_names)
                                             .Include(p => p.permissions)
                                                 .ThenInclude(permissions => permissions.actions)
                                             .Include(u => u.create_user)
                                             .Include(u => u.update_user);

            return await RolePermissions.ToListAsync();
        }

        public async Task<RolePermissionsModels> ReadOneAsync(int id)
        {
            var RolePermissions = await _context.role_permissions
                .Include(r => r.roles)
                .Include(p => p.permissions)
                    .ThenInclude(permissions => permissions.function_names)
                .Include(p => p.permissions)
                    .ThenInclude(permissions => permissions.actions)
                .Include(u => u.create_user)
                .Include(u => u.update_user)
                .SingleOrDefaultAsync(x => x.role_id == id);

            return RolePermissions;
        }

        public async Task UpdateAsync(RolePermissionsModels RolePermissions)
        {
            _context.role_permissions.Update(RolePermissions);
            await _context.SaveChangesAsync();
        }

        public async Task<Boolean> DeleteAsync(IEnumerable<RolePermissionsModels> RolePermissions)
        {
            _context.role_permissions.RemoveRange(RolePermissions);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}