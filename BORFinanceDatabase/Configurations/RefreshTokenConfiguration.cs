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

    public class RefreshTokenConfiguration
        : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TokenHash)
                .HasMaxLength(128)
                .IsRequired();

            builder.HasIndex(x => x.TokenHash)
                .IsUnique();

            builder.Property(x => x.ExpiresAt)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(x => x.IsRevoked)
                .HasDefaultValue(false);

            builder.Property(x => x.ReplacedByTokenHash)
                .HasMaxLength(128);

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
