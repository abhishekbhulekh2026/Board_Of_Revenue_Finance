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
    public class RolePermissionConfiguration
    : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(
            EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable("RolePermissions");

            // Composite Primary Key
            builder.HasKey(x => new
            {
                x.RoleId,
                x.PermissionId
            });

            builder.Property(x => x.IsAllowed)
                .HasDefaultValue(true);

            builder.Property(x => x.AssignedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Role relationship
            builder.HasOne(x => x.Role)
                .WithMany(x => x.RolePermissions)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            // Permission relationship
            builder.HasOne(x => x.Permission)
                .WithMany(x => x.RolePermissions)
                .HasForeignKey(x => x.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.PermissionId);
        }
    }
}
