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
    public class DesignationConfiguration
      : IEntityTypeConfiguration<Designation>
    {
        public void Configure(EntityTypeBuilder<Designation> builder)
        {
            builder.ToTable("Designations");

            builder.HasKey(x => x.DesignationId);

            builder.Property(x => x.DesignationCode)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.HasIndex(x => x.DesignationCode)
                   .IsUnique();

            builder.Property(x => x.DesignationName)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(x => x.DesignationNameHi)
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
