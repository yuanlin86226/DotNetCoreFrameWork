using System;
using System.Linq;
using System.Threading.Tasks;
using Context;
using Models;

namespace Seed
{
    public class RolePermissionsSeeder
    {
        public RolePermissionsSeeder(CustomContext context)
        {
            if (!context.role_permissions.Any())
            {
                Task.Run(async () =>
                {
                    for (int i = 1; i < 21; i++)
                    {
                        await context.role_permissions.AddAsync(new RolePermissionsModels() { role_id = 1, permission_id = i, create_time = DateTime.Now, update_time = DateTime.Now });
                        await context.SaveChangesAsync();
                    }
                }).Wait();
            }
        }
    }
}