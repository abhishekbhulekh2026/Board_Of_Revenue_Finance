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
    public class UserSessionConfiguration
     : IEntityTypeConfiguration<UserSession>
    {
        public void Configure(EntityTypeBuilder<UserSession> builder)
        {
            builder.ToTable("UserSessions");

            builder.HasKey(x => x.UserSessionId);

            builder.Property(x => x.RefreshTokenHash)
                   .HasMaxLength(500)
                   .IsRequired();

            builder.HasIndex(x => x.RefreshTokenHash)
                   .IsUnique();

            builder.Property(x => x.IpAddress)
                   .HasMaxLength(45);

            builder.Property(x => x.UserAgent)
                   .HasMaxLength(500);

            builder.Property(x => x.DeviceName)
                   .HasMaxLength(200);

            builder.Property(x => x.IsActive)
                   .HasDefaultValue(true);

            builder.Property(x => x.CreatedAt)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // User → Sessions
            builder.HasOne(x => x.User)
                   .WithMany(x => x.UserSessions)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Useful indexes
            builder.HasIndex(x => x.UserId);

            builder.HasIndex(x => x.ExpiresAt);
        }
    }
}
