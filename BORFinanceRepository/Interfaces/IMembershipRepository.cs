using BORFinanceDomain.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceRepository.Interfaces
{
    public interface IMembershipRepository
    : IRepository<Membership, long>
    {
        Task<bool> ExistsByEmployeeIdAsync(long employeeId);

        Task<bool> ExistsByMembershipNumberAsync(
            string membershipNumber);

        Task<bool> ExistsByMembershipNumberAsync(
            string membershipNumber,
            long membershipId);

        Task<bool> HasLoansAsync(long membershipId);

        Task<bool> HasFixedDepositsAsync(long membershipId);
    }
}
