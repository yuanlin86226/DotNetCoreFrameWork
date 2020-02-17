using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models.EntityConfiguration
{
    public class FunctionNamesEntityConfiguration : IEntityTypeConfiguration<FunctionNamesModels>
    {
        public void Configure(EntityTypeBuilder<FunctionNamesModels> builder)
        {
            builder.ToTable("function_names"); //對應資料表的資料結構

            //FunctionNames 一對多 Users
            builder.HasOne(c => c.create_user)
                   .WithMany(f => f.FunctionNamesCreateUser)
                   .HasForeignKey(c => c.create_user_id);

            //FunctionNames 一對多 Users
            builder.HasOne(u => u.update_user)
                   .WithMany(f => f.FunctionNamesUpdateUser)
                   .HasForeignKey(u => u.update_user_id);
        }
    }
}