using System;
using System.Linq;
using System.Threading.Tasks;
using Context;
using Models;

namespace Seed
{
    public class PermissionsSeeder
    {
        public PermissionsSeeder(CustomContext context)
        {
            if (!context.permissions.Any())
            {
                Task.Run(async () =>
                {
                    for (int i = 1; i < 6; i++)
                    {
                        for (int j = 1; j < 5; j++)
                        {
                            await context.permissions.AddAsync(new PermissionsModels() { function_name_id = i, action_id = j, create_time = DateTime.Now, update_time = DateTime.Now });
                            await context.SaveChangesAsync();
                        }
                    }
                }).Wait();
            }
        }
    }
}