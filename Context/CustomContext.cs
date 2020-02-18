using Microsoft.EntityFrameworkCore;
using Models;
using Models.EntityConfiguration;

namespace Context
{
    public class CustomContext : DbContext
    {
        public CustomContext(DbContextOptions<CustomContext> options) : base(options)
        {
        }
        #region 連接對應資料表的Models
        //連接對應資料表的Models
        public DbSet<UsersModels> users { get; set; } //使用者
        public DbSet<RolesModels> roles { get; set; } //角色
        public DbSet<PermissionsModels> permissions { get; set; } //權限
        public DbSet<FunctionNamesModels> function_names { get; set; } //功能名稱
        public DbSet<ActionsModels> actions { get; set; } //動作
        public DbSet<RolePermissionsModels> role_permissions { get; set; } //角色的權限
        public DbSet<CheckinLogsModels> checkin_logs { get; set; } //角色的權限
        #endregion

        #region 建立資料關聯
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ActionsEntityConfiguration());
            modelBuilder.ApplyConfiguration(new FunctionNamesEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionsEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RolePermissionsEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RolesEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UsersEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CheckinLogsEntityConfiguration());
        }
        #endregion
    }
}

