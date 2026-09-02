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
    public class LoanInstallmentRepository
     : Repository<LoanInstallment, long>,
       ILoanInstallmentRepository
    {
        public LoanInstallmentRepository(
            BORFinanceDbContext context)
            : base(context)
        {
        }

        public async Task<bool> ExistsByInstallmentNumberAsync(
            long loanId,
            int installmentNumber)
        {
            return await _context.LoanInstallments
                .AnyAsync(x =>
                    x.LoanId == loanId &&
                    x.InstallmentNumber == installmentNumber);
        }

        public async Task<bool> ExistsByInstallmentNumberAsync(
            long loanId,
            int installmentNumber,
            long installmentId)
        {
            return await _context.LoanInstallments
                .AnyAsync(x =>
                    x.LoanId == loanId &&
                    x.InstallmentNumber == installmentNumber &&
                    x.LoanInstallmentId != installmentId);
        }

        public async Task<bool> HasPaymentsAsync(
            long installmentId)
        {
            return await _context.LoanInstallments
                .AnyAsync(x =>
                    x.LoanInstallmentId == installmentId &&
                    x.PaidAmount > 0);
        }

        public async Task<IEnumerable<LoanInstallment>> GetLoanInstallmentByLoanIdAsync(
            long loanId)
        {
            return await _context.LoanInstallments
                .Where(x => x.LoanId == loanId)
                .ToListAsync();
        }
    }
}
