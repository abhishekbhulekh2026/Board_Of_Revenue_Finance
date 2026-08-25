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
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("MasterRole");

            builder.HasKey(x => x.RoleId);

            builder.Property(x => x.RoleCode)
                   .HasMaxLength(30)
                   .IsRequired();

            builder.HasIndex(x => x.RoleCode)
                   .IsUnique();

            builder.Property(x => x.RoleName)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.RoleDescription)
                   .HasMaxLength(500);

            builder.Property(x => x.RoleNameHi)
                   .HasMaxLength(300);

            builder.Property(x => x.RoleLevel)
                   .HasDefaultValue((byte)0);

            builder.Property(x => x.SortOrder)
                   .HasDefaultValue(0);

            builder.Property(x => x.IsSystemRole)
                   .HasDefaultValue(false);

            builder.Property(x => x.IsAssignable)
                   .HasDefaultValue(true);

            builder.Property(x => x.IsActive)
                   .HasDefaultValue(true);

            builder.Property(x => x.CreatedAt)
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasOne(x => x.ParentRole)
                   .WithMany(x => x.ChildRoles)
                   .HasForeignKey(x => x.ParentRoleId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
