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
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions");

            builder.HasKey(x => x.PermissionId);

            builder.Property(x => x.PermissionCode)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.HasIndex(x => x.PermissionCode)
                   .IsUnique();

            builder.Property(x => x.PermissionName)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(x => x.ModuleName)
                   .HasMaxLength(100);

            builder.Property(x => x.Description)
                   .HasMaxLength(500);

            builder.Property(x => x.IsActive)
                   .HasDefaultValue(true);

            builder.Property(x => x.CreatedAt)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
