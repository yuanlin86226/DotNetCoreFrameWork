using System;
using System.Linq;
using System.Threading.Tasks;
using Context;
using Models;

namespace Seed
{
    public class ActionsSeeder
    {
        public ActionsSeeder(CustomContext context)
        {
            if (!context.actions.Any())
            {
                Task.Run(async () =>
                {
                    await context.actions.AddAsync(new ActionsModels() { action = "新增", create_time = DateTime.Now, update_time = DateTime.Now });
                    await context.SaveChangesAsync();
                    await context.actions.AddAsync(new ActionsModels() { action = "查詢", create_time = DateTime.Now, update_time = DateTime.Now });
                    await context.SaveChangesAsync();
                    await context.actions.AddAsync(new ActionsModels() { action = "修改", create_time = DateTime.Now, update_time = DateTime.Now });
                    await context.SaveChangesAsync();
                    await context.actions.AddAsync(new ActionsModels() { action = "刪除", create_time = DateTime.Now, update_time = DateTime.Now });
                    await context.SaveChangesAsync();
                }).Wait();
            }
        }
    }
}