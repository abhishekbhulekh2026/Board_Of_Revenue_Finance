using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BORFinanceDomain.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDatabase.Configurations
{
    public class LoginHistoryConfiguration
     : IEntityTypeConfiguration<LoginHistory>
    {
        public void Configure(EntityTypeBuilder<LoginHistory> builder)
        {
            builder.ToTable("LoginHistory");

            builder.HasKey(x => x.LoginHistoryId);

            builder.Property(x => x.Username)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.FailureReason)
                   .HasMaxLength(500);

            builder.Property(x => x.IpAddress)
                   .HasMaxLength(45);

            builder.Property(x => x.UserAgent)
                   .HasMaxLength(500);

            builder.Property(x => x.DeviceName)
                   .HasMaxLength(200);

            builder.Property(x => x.LoginDate)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasIndex(x => x.UserId);

            builder.HasIndex(x => x.LoginDate);

            builder.HasOne(x => x.User)
                   .WithMany(x => x.LoginHistories)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
