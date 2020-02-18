using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models.EntityConfiguration
{
    public class CheckinLogsEntityConfiguration : IEntityTypeConfiguration<CheckinLogsModels>
    {
        public void Configure(EntityTypeBuilder<CheckinLogsModels> builder)
        {
            builder.ToTable("checkin_logs"); //對應資料表的資料結構

            //CheckinLog 一對多 Users
            builder.HasOne(f => f.users)
                   .WithMany(p => p.CheckinLogs)
                   .HasForeignKey(f => f.user_id);

            //CheckinLog 一對多 Users
            builder.HasOne(c => c.create_user)
                   .WithMany(p => p.CheckinLogsCreateUser)
                   .HasForeignKey(c => c.create_user_id);

            //CheckinLog 一對多 Users
            builder.HasOne(u => u.update_user)
                   .WithMany(p => p.CheckinLogsUpdateUser)
                   .HasForeignKey(u => u.update_user_id);
        }
    }
}