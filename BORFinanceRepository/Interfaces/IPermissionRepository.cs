using BORFinanceDomain.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceRepository.Interfaces
{
    public interface IPermissionRepository
     : IRepository<Permission, int>
    {
        Task<Permission?> GetByCodeAsync(
            string permissionCode);

        Task<IEnumerable<Permission>>
            GetActivePermissionsAsync();

        Task<bool> ExistsByCodeAsync(
            string permissionCode);

        Task<bool> HasRolesAsync(
            int permissionId);
    }
}
