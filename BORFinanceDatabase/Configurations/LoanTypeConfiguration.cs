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
    public class LoanTypeConfiguration
     : IEntityTypeConfiguration<LoanType>
    {
        public void Configure(EntityTypeBuilder<LoanType> builder)
        {
            builder.ToTable("LoanTypes");

            builder.HasKey(x => x.LoanTypeId);

            builder.Property(x => x.LoanTypeCode)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(x => x.LoanTypeCode)
                .IsUnique();

            builder.Property(x => x.LoanTypeName)
                .HasMaxLength(150)
                .IsRequired();

            builder.HasIndex(x => x.LoanTypeName)
                .IsUnique();

            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // LoanType → Loans
            builder.HasMany(x => x.Loans)
                .WithOne(x => x.LoanType)
                .HasForeignKey(x => x.LoanTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
