using BORFinanceDomain.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BORFinanceDomain.Entities.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceDatabase.Configurations
{
    
    public class MembershipConfiguration
        : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder.ToTable("Memberships");

            builder.HasKey(x => x.MembershipId);

            builder.Property(x => x.IsDeleted)
    .HasDefaultValue(false);

            builder.Property(x => x.MembershipNumber)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.HasIndex(x => x.MembershipNumber)
                   .IsUnique();

            builder.Property(x => x.MembershipDate)
                   .IsRequired();

            builder.Property(x => x.ShareCount)
                   .HasDefaultValue(0);

            builder.Property(x => x.ShareValue)
                   .HasPrecision(18, 2)
                   .HasDefaultValue(0);

            builder.Property(x => x.Status)
                   .HasMaxLength(30)
                   .HasDefaultValue("Active");

            builder.Property(x => x.CreatedAt)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasIndex(x => x.EmployeeId)
       .IsUnique();

            // Employee → Membership
            builder.HasOne(x => x.Employee)
                   .WithOne(x => x.Membership)
                   .HasForeignKey<Membership>(x => x.EmployeeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
