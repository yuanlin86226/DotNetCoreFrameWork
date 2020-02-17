using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models.EntityConfiguration
{
    public class RolePermissionsEntityConfiguration : IEntityTypeConfiguration<RolePermissionsModels>
    {
        public void Configure(EntityTypeBuilder<RolePermissionsModels> builder)
        {
            builder.ToTable("role_permissions"); //對應資料表的資料結構
            //RolePermissions 一對多 Roles
            builder.HasOne(r => r.roles)
                   .WithMany(r => r.RolePermissions)
                   .HasForeignKey(r => r.role_id)
                   .OnDelete(DeleteBehavior.SetNull);

            //RolePermissions 一對多 Permissions
            builder.HasOne(p => p.permissions)
                   .WithMany(r => r.RolePermissions)
                   .HasForeignKey(p => p.permission_id)
                   .OnDelete(DeleteBehavior.SetNull);
            
            //RolePermissions 一對多 Users
            builder.HasOne(c => c.create_user)
                   .WithMany(r => r.RolePermissionsCreateUser)
                   .HasForeignKey(c => c.create_user_id);

            //RolePermissions 一對多 Users
            builder.HasOne(u => u.update_user)
                   .WithMany(r => r.RolePermissionsUpdateUser)
                   .HasForeignKey(u => u.update_user_id);
        }
    }
}