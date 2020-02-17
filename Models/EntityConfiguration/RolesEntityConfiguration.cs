using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models.EntityConfiguration
{
    public class RolesEntityConfiguration : IEntityTypeConfiguration<RolesModels>
    {
        public void Configure(EntityTypeBuilder<RolesModels> builder)
        {
            builder.ToTable("roles"); //對應資料表的資料結構
            //Roles 一對多 Users
            builder.HasOne(c => c.create_user)
                   .WithMany(r => r.RolesCreateUser)
                   .HasForeignKey(c => c.create_user_id);

            //Roles 一對多 Users
            builder.HasOne(u => u.update_user)
                   .WithMany(r => r.RolesUpdateUser)
                   .HasForeignKey(u => u.update_user_id);
        }
    }
}