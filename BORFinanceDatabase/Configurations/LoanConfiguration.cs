using BORFinanceDomain.Entities.Employees;
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
    public class LoanConfiguration : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.ToTable("Loans");

            builder.HasKey(x => x.LoanId);

            builder.Property(x => x.LoanNumber)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.HasIndex(x => x.LoanNumber)
                   .IsUnique();

            builder.Property(x => x.RequestedAmount)
                   .HasPrecision(18, 2);

            builder.Property(x => x.ApprovedAmount)
                   .HasPrecision(18, 2);

            builder.Property(x => x.InterestRate)
                   .HasPrecision(5, 2);

            builder.Property(x => x.PaidAmount)
                   .HasPrecision(18, 2);

            builder.Property(x => x.Status)
                   .HasMaxLength(50)
                   .HasDefaultValue("Pending");

            builder.Property(x => x.PaymentStatus)
                   .HasMaxLength(50);

            builder.Property(x => x.PaymentReference)
                   .HasMaxLength(100);

            builder.Property(x => x.BankTransactionReference)
                   .HasMaxLength(150);

            // Loan → Membership
            builder.HasOne(x => x.Membership)
                   .WithMany(x => x.Loans)
                   .HasForeignKey(x => x.MembershipId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Loan → LoanType
            builder.HasOne(x => x.LoanType)
                   .WithMany(x => x.Loans)
                   .HasForeignKey(x => x.LoanTypeId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Loan → LoanInstallment
            builder.HasMany(x => x.Installments)
                   .WithOne(x => x.Loan)
                   .HasForeignKey(x => x.LoanId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Loan → LoanGuarantor
            builder.HasMany(x => x.Guarantors)
                   .WithOne(x => x.Loan)
                   .HasForeignKey(x => x.LoanId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
