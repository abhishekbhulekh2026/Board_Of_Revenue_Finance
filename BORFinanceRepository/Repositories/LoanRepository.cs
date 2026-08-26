using BORFinanceDomain.Loans;
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
    public class LoanRepository
      : Repository<Loan, long>,
        ILoanRepository
    {
        public LoanRepository(BORFinanceDbContext context)
            : base(context)
        {
        }

        public async Task<bool> ExistsByLoanNumberAsync(
            string loanNumber)
        {
            return await _context.Loans
                .AnyAsync(x =>
                    x.LoanNumber == loanNumber);
        }

        public async Task<bool> ExistsByLoanNumberAsync(
            string loanNumber,
            long loanId)
        {
            return await _context.Loans
                .AnyAsync(x =>
                    x.LoanNumber == loanNumber &&
                    x.LoanId != loanId);
        }

        public async Task<bool> HasInstallmentsAsync(
            long loanId)
        {
            return await _context.LoanInstallments
                .AnyAsync(x =>
                    x.LoanId == loanId);
        }

        public async Task<bool> MembershipExistsAsync(long membershipId)
        {
            return await _context.Memberships
                .AnyAsync(x => x.MembershipId == membershipId);
        }

        public async Task<bool> IsMembershipActiveAsync(long membershipId)
        {
            return await _context.Memberships
                .AnyAsync(x =>
                    x.MembershipId == membershipId &&
                    x.Status == "Active");
        }
    }
}
