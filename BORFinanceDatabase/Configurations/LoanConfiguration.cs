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

            builder.Property(x => x.LoanType)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(x => x.RequestedAmount)
                   .HasPrecision(18, 2);

            builder.Property(x => x.ApprovedAmount)
                   .HasPrecision(18, 2);

            builder.Property(x => x.InterestRate)
                   .HasPrecision(8, 4);

            builder.Property(x => x.Status)
                   .HasMaxLength(30)
                   .HasDefaultValue("Pending");

            builder.Property(x => x.Purpose)
                   .HasMaxLength(500);

            builder.Property(x => x.Remarks)
                   .HasMaxLength(1000);

            builder.Property(x => x.ApplicationDate)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(x => x.CreatedAt)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasIndex(x => x.MembershipId);

            builder.HasIndex(x => x.Status);

           
            builder.HasOne(x => x.Membership)
    .WithMany(x => x.Loans)
    .HasForeignKey(x => x.MembershipId)
    .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.LoanNumber)
                .IsUnique();

            builder.HasIndex(x => x.MembershipId);
        }
    }
}
