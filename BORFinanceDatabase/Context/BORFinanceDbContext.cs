using Microsoft.EntityFrameworkCore;
using BORFinanceDomain.Entities;
using BORFinanceDomain.Entities.Employees;
using BORFinanceDomain.Entities.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using BORFinanceDomain.Loans;
using BORFinanceDomain.Members;
using BORFinanceDomain.FixedDeposits;

namespace SchoolDatabase.Context
{
    public class BORFinanceDbContext : DbContext
    {
        public BORFinanceDbContext(DbContextOptions<BORFinanceDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
        public DbSet<UserSession> UserSessions => Set<UserSession>();
        public DbSet<LoginHistory> LoginHistories => Set<LoginHistory>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Designation> Designations => Set<Designation>();
        public DbSet<Employee> Employees => Set<Employee>();

        public DbSet<Membership> Memberships => Set<Membership>();
        public DbSet<Loan> Loans => Set<Loan>();
        public DbSet<LoanInstallment> LoanInstallments => Set<LoanInstallment>();
        public DbSet<FixedDeposit> FixedDeposits => Set<FixedDeposit>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BORFinanceDbContext).Assembly);

            // Scan all entities configuration and apply soft delete automatically
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
                {
                    // Dynamic equivalent of: builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
                    modelBuilder.Entity(entityType.ClrType)
                        .HasQueryFilter(ConvertFilterExpression(entityType.ClrType));
                }
            }
        }

        // Helper method to build the expression tree dynamically
        private static LambdaExpression ConvertFilterExpression(Type type)
        {
            var parameter = Expression.Parameter(type, "e");
            var property = Expression.Property(parameter, nameof(ISoftDelete.IsDeleted));
            var falseConstant = Expression.Constant(false);
            var compare = Expression.Equal(property, falseConstant);

            return Expression.Lambda(compare, parameter);
        }
    }
}
