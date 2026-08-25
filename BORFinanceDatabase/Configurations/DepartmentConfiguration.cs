using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BORFinanceDomain.Entities.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDatabase.Configurations
{
    public class DepartmentConfiguration
      : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments");

            builder.HasKey(x => x.DepartmentId);

            builder.Property(x => x.DepartmentCode)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.HasIndex(x => x.DepartmentCode)
                   .IsUnique();

            builder.Property(x => x.DepartmentName)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(x => x.DepartmentNameHi)
                   .HasMaxLength(200);

            builder.Property(x => x.Description)
                   .HasMaxLength(500);

            builder.Property(x => x.SortOrder)
                   .HasDefaultValue(0);

            builder.Property(x => x.IsActive)
                   .HasDefaultValue(true);

            builder.Property(x => x.CreatedAt)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasIndex(x => x.IsActive);
        }
    }
}
