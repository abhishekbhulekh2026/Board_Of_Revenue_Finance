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
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class EmployeeConfiguration
        : IEntityTypeConfiguration<Employee>
    {
        public void Configure(
            EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(x => x.EmployeeId);

            builder.Property(x => x.EmployeeCode)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.FullName)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.FullNameHi)
                .HasMaxLength(300);

            builder.Property(x => x.FatherName)
                .HasMaxLength(200);

            builder.Property(x => x.MobileNumber)
                .HasMaxLength(20);

            builder.Property(x => x.Email)
                .HasMaxLength(200);

            builder.Property(x => x.Address)
                .HasMaxLength(500);

            builder.Property(x => x.BasicSalary)
                .HasPrecision(18, 2);

            builder.Property(x => x.EmployeeStatus)
                .HasMaxLength(50);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // EmployeeCode must be unique
            builder.HasIndex(x => x.EmployeeCode)
                .IsUnique();

            // User relationship
            //builder.HasOne(x => x.User)
            //    .WithMany()
            //    .HasForeignKey(x => x.UserId)
            //    .OnDelete(DeleteBehavior.SetNull);

            // One User <-> One Employee
            builder.HasOne(x => x.User)
                .WithOne(x => x.Employee)
                .HasForeignKey<Employee>(x => x.UserId)
                .OnDelete(DeleteBehavior.SetNull);




            // Department relationship
            builder.HasOne(x => x.Department)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Designation relationship
            builder.HasOne(x => x.Designation)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.DesignationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.DepartmentId);
            builder.HasIndex(x => x.DesignationId);
        }
    }
}
