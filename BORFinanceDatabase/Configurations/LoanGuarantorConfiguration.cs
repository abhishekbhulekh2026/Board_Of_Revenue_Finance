using BORFinanceDomain.Loans;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceDatabase.Configurations
{
    public class LoanGuarantorConfiguration
    : IEntityTypeConfiguration<LoanGuarantor>
    {
        public void Configure(EntityTypeBuilder<LoanGuarantor> builder)
        {
            builder.ToTable("LoanGuarantors");

            builder.HasKey(x => x.LoanGuarantorId);

            builder.Property(x => x.GuarantorName)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(x => x.Relationship)
                   .HasMaxLength(100);

            builder.Property(x => x.MobileNumber)
                   .HasMaxLength(20);

            builder.Property(x => x.Address)
                   .HasMaxLength(500);

            builder.Property(x => x.IsApproved)
                   .HasDefaultValue(false);

            builder.Property(x => x.CreatedAt)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Loan → LoanGuarantor
            builder.HasOne(x => x.Loan)
                   .WithMany(x => x.Guarantors)
                   .HasForeignKey(x => x.LoanId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Membership → LoanGuarantor
            builder.HasOne(x => x.Membership)
                   .WithMany()
                   .HasForeignKey(x => x.MembershipId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Employee → LoanGuarantor
            builder.HasOne(x => x.Employee)
                   .WithMany()
                   .HasForeignKey(x => x.EmployeeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
