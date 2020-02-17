using System;
using System.Threading.Tasks;
using Context;
using Microsoft.Extensions.DependencyInjection;

namespace Seed
{
    public static class DataSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<CustomContext>(); //透過IServiceProvider實現DI注入

            ActionsSeeder Actions = new ActionsSeeder(context);
            FunctionNamesSeeder functionNames = new FunctionNamesSeeder(context);
            PermissionsSeeder Permissions = new PermissionsSeeder(context);
            RolesSeeder Roels = new RolesSeeder(context);
            UsersSeeder users = new UsersSeeder(context);
            RolePermissionsSeeder RolePermissions = new RolePermissionsSeeder(context);
        }
    }
}