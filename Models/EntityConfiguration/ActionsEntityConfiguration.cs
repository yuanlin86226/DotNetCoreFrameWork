using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models.EntityConfiguration
{
    public class ActionsEntityConfiguration : IEntityTypeConfiguration<ActionsModels>
    {
        public void Configure(EntityTypeBuilder<ActionsModels> builder)
        {
            builder.ToTable("actions"); //對應資料表的資料結構

            //Actions 一對多 Users
            builder.HasOne(c => c.create_user)
                   .WithMany(a => a.ActionsCreateUser)
                   .HasForeignKey(c => c.create_user_id);

            //Actions 一對多 Users
            builder.HasOne(u => u.update_user)
                   .WithMany(a => a.ActionsUpdateUser)
                   .HasForeignKey(u => u.update_user_id);
        }
    }
}