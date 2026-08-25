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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Username)
                   .IsUnique();

            builder.Property(x => x.Username)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.PasswordHash)
                   .HasMaxLength(500)
                   .IsRequired();

            builder.Property(x => x.FullName)
                   .HasMaxLength(500)
                   .IsRequired();

            builder.Property(x => x.MobileNumber)
                   .HasMaxLength(20);

            builder.Property(x => x.Email)
                   .HasMaxLength(200);

            builder.Property(x => x.ProfilePic)
                   .HasMaxLength(1000);

            builder.Property(x => x.ApprovalStatus)
                   .HasMaxLength(50)
                   .HasDefaultValue("Pending");

            builder.Property(x => x.FailedLoginAttempts)
                   .HasDefaultValue(0);

            builder.Property(x => x.AccountLocked)
                   .HasDefaultValue(false);

            builder.Property(x => x.IsActive)
                   .HasDefaultValue(true);

            builder.Property(x => x.IsDeleted)
                   .HasDefaultValue(false);

            builder.Property(x => x.CreatedDate)
                     .HasDefaultValueSql("CURRENT_TIMESTAMP");

            //builder.HasOne(x => x.Role)
            //       .WithMany(x => x.Users)
            //       .HasForeignKey(x => x.RoleId)
            //       .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
