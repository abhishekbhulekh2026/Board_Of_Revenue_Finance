using BORFinanceDomain.Loans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceRepository.Interfaces
{
    public interface ILoanInstallmentRepository
    : IRepository<LoanInstallment, long>
    {
        Task<bool> ExistsByInstallmentNumberAsync(
            long loanId,
            int installmentNumber);

        Task<bool> ExistsByInstallmentNumberAsync(
            long loanId,
            int installmentNumber,
            long installmentId);

        Task<bool> HasPaymentsAsync(long installmentId);

        Task<IEnumerable<LoanInstallment>> GetLoanInstallmentByLoanIdAsync(
            long loanId);
    }
}
