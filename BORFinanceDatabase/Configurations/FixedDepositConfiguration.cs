using BORFinanceDomain.FixedDeposits;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceDatabase.Configurations
{
    public class FixedDepositConfiguration
     : IEntityTypeConfiguration<FixedDeposit>
    {
        public void Configure(EntityTypeBuilder<FixedDeposit> builder)
        {
            builder.ToTable("FixedDeposits");

            builder.HasKey(x => x.FixedDepositId);

            builder.Property(x => x.FDNumber)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.HasIndex(x => x.FDNumber)
                   .IsUnique();

            builder.Property(x => x.DepositAmount)
                   .HasPrecision(18, 2);

            builder.Property(x => x.InterestRate)
                   .HasPrecision(8, 4);

            builder.Property(x => x.MaturityAmount)
                   .HasPrecision(18, 2);

            builder.Property(x => x.InterestPayoutType)
                   .HasMaxLength(30)
                   .IsRequired();

            builder.Property(x => x.Status)
                   .HasMaxLength(30)
                   .HasDefaultValue("Active");

            builder.Property(x => x.ClosedAmount)
                   .HasPrecision(18, 2);

            builder.Property(x => x.Remarks)
                   .HasMaxLength(500);

            builder.Property(x => x.CreatedAt)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasIndex(x => x.MembershipId);

            builder.HasIndex(x => x.MaturityDate);

            builder.HasIndex(x => x.Status);

            builder.HasOne(x => x.Membership)
                   .WithMany(x => x.FixedDeposits)
                   .HasForeignKey(x => x.MembershipId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
