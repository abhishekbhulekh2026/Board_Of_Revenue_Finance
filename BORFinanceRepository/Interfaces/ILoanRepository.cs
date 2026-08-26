using BORFinanceDomain.Loans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceRepository.Interfaces
{
    public interface ILoanRepository
      : IRepository<Loan, long>
    {
        Task<bool> ExistsByLoanNumberAsync(
            string loanNumber);

        Task<bool> ExistsByLoanNumberAsync(
            string loanNumber,
            long loanId);

        Task<bool> HasInstallmentsAsync(
            long loanId);

        Task<bool> MembershipExistsAsync(long membershipId);

        Task<bool> IsMembershipActiveAsync(long membershipId);
    }
}
