using BORFinanceDomain.Members;
using BORFinanceRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using SchoolDatabase.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceRepository.Repositories
{
    public class MembershipRepository
     : Repository<Membership, long>,
       IMembershipRepository
    {
        public MembershipRepository(
            BORFinanceDbContext context)
            : base(context)
        {
        }

        public async Task<bool> ExistsByEmployeeIdAsync(
            long employeeId)
        {
            return await _context.Memberships
                .AnyAsync(x =>
                    x.EmployeeId == employeeId);
        }

        public async Task<bool> ExistsByMembershipNumberAsync(
            string membershipNumber)
        {
            return await _context.Memberships
                .AnyAsync(x =>
                    x.MembershipNumber == membershipNumber);
        }

        public async Task<bool> ExistsByMembershipNumberAsync(
            string membershipNumber,
            long membershipId)
        {
            return await _context.Memberships
                .AnyAsync(x =>
                    x.MembershipNumber == membershipNumber &&
                    x.MembershipId != membershipId);
        }

        public async Task<bool> HasLoansAsync(
            long membershipId)
        {
            return await _context.Loans
                .AnyAsync(x =>
                    x.MembershipId == membershipId);
        }

        public async Task<bool> HasFixedDepositsAsync(
            long membershipId)
        {
            return await _context.FixedDeposits
                .AnyAsync(x =>
                    x.MembershipId == membershipId);
        }
    }
}
