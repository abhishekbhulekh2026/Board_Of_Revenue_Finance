using BORFinanceDomain.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceRepository.Interfaces
{
    public interface IRolePermissionRepository
    {
        Task<RolePermission?> GetAsync(
            int roleId,
            int permissionId);

        Task<IEnumerable<RolePermission>> GetByRoleIdAsync(
            int roleId);

        Task<IEnumerable<RolePermission>> GetByPermissionIdAsync(
            int permissionId);

        Task<bool> ExistsAsync(
            int roleId,
            int permissionId);

        Task<bool> IsAllowedAsync(
            int roleId,
            int permissionId);

        Task AddAsync(RolePermission rolePermission);

        void Update(RolePermission rolePermission);

        void Delete(RolePermission rolePermission);

        Task<bool>  RoleExistsAsync(
          int roleId);

        Task<bool> PermissionExistsAsync(
           int permissionId);
    }
}
