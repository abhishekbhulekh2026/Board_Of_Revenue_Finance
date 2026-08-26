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
    public class LoanInstallmentConfiguration
      : IEntityTypeConfiguration<LoanInstallment>
    {
        public void Configure(EntityTypeBuilder<LoanInstallment> builder)
        {
            builder.ToTable("LoanInstallments");

            builder.HasKey(x => x.LoanInstallmentId);

            builder.Property(x => x.PrincipalAmount)
                   .HasPrecision(18, 2);

            builder.Property(x => x.InterestAmount)
                   .HasPrecision(18, 2);

            builder.Property(x => x.InstallmentAmount)
                   .HasPrecision(18, 2);

            builder.Property(x => x.PaidAmount)
                   .HasPrecision(18, 2)
                   .HasDefaultValue(0);

            builder.Property(x => x.Status)
                   .HasMaxLength(30)
                   .HasDefaultValue("Pending");

            builder.HasIndex(x => new
            {
                x.LoanId,
                x.InstallmentNumber
            })
            .IsUnique();

            builder.HasIndex(x => x.DueDate);

            builder.HasOne(x => x.Loan)
                   .WithMany(x => x.Installments)
                   .HasForeignKey(x => x.LoanId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
