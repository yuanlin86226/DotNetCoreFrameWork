using System;
using System.Linq;
using System.Threading.Tasks;
using Context;
using Models;
using Utils;

namespace Seed
{
    public class UsersSeeder
    {
        public UsersSeeder(CustomContext context)
        {
            if (!context.users.Any())
            {
                Task.Run(async () =>
                {
                    Guid UUID = Guid.NewGuid();
                    MD5HashUtils MD5 = new MD5HashUtils();
                    string Md5Password = MD5.MD5Hash("admin");
                    await context.users.AddAsync(new UsersModels() { user_id = UUID.ToString(), account_number = "admin", password = Md5Password, user_name = "管理者", role_id = 1, phone = null, email = null, gender = "男", due_date = DateTime.Now, resignation_date = null , create_date = DateTime.Now});
                    await context.SaveChangesAsync();
                }).Wait();
            }
        }
    }
}