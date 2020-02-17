using System;
using System.Linq;
using System.Threading.Tasks;
using Context;
using Models;

namespace Seed
{
    public class RolesSeeder
    {
        public RolesSeeder(CustomContext context)
        {
            if (!context.roles.Any())
            {
                Task.Run(async ()=>{
                    await context.roles.AddAsync(new RolesModels() { role = "管理者", create_time = DateTime.Now, update_time = DateTime.Now });
                    await context.SaveChangesAsync();
                }).Wait();
            }
        }
    }
}