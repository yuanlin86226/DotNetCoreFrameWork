using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models.EntityConfiguration
{
    public class PermissionsEntityConfiguration : IEntityTypeConfiguration<PermissionsModels>
    {
        public void Configure(EntityTypeBuilder<PermissionsModels> builder)
        {
            builder.ToTable("permissions"); //對應資料表的資料結構

            //Permissions 一對多 FunctionNames
            builder.HasOne(f => f.function_names)
                   .WithMany(p => p.Permissions)
                   .HasForeignKey(f => f.function_name_id)
                   .OnDelete(DeleteBehavior.SetNull);

            //Permissions 一對多 Actions
            builder.HasOne(a => a.actions)
                   .WithMany(p => p.Permissions)
                   .HasForeignKey(a => a.action_id)
                   .OnDelete(DeleteBehavior.SetNull);

            //Permissions 一對多 Users
            builder.HasOne(c => c.create_user)
                   .WithMany(p => p.PermissionsCreateUser)
                   .HasForeignKey(c => c.create_user_id);

            //Permissions 一對多 Users
            builder.HasOne(u => u.update_user)
                   .WithMany(p => p.PermissionsUpdateUser)
                   .HasForeignKey(u => u.update_user_id);
        }
    }
}