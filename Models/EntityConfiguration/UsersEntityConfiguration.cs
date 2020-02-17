using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models.EntityConfiguration
{
    public class UsersEntityConfiguration : IEntityTypeConfiguration<UsersModels>
    {
        public void Configure(EntityTypeBuilder<UsersModels> builder)
        {
            builder.ToTable("users"); //對應資料表的資料結構
            //Users 一對多 Roles
            builder.HasOne(r => r.roles)
                   .WithMany(u => u.Users)
                   .HasForeignKey(r => r.role_id)
                   .OnDelete(DeleteBehavior.SetNull);
            
            //將account_number欄位設定為唯一值
            builder.HasIndex(u => u.account_number)
                   .IsUnique();
        }
    }
}