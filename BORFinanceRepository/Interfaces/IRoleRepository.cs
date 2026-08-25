using BORFinanceDomain.Entities.Security;
using BORFinanceRepository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceRepository.Interfaces
{
    public interface IRoleRepository : IRepository<Role, int>
    {
        Task<Role?> GetByCodeAsync(string roleCode);

        Task<IEnumerable<Role>> GetActiveRolesAsync();

        Task<bool> HasUsersAsync(int roleId);

        Task<bool> ExistsByCodeAsync(string roleCode);

    }
}
