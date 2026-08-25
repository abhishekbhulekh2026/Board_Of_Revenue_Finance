using Microsoft.EntityFrameworkCore;
using SchoolDatabase.Context;
using BORFinanceDomain.Entities.Security;
using BORFinanceRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceRepository.Repositories
{
    public class PermissionRepository
    : Repository<Permission, int>,
      IPermissionRepository
    {
        public PermissionRepository(
            BORFinanceDbContext context)
            : base(context)
        {
        }

        public async Task<Permission?> GetByCodeAsync(
            string permissionCode)
        {
            return await _context.Permissions
                .FirstOrDefaultAsync(x =>
                    x.PermissionCode == permissionCode);
        }

        public async Task<IEnumerable<Permission>>
            GetActivePermissionsAsync()
        {
            return await _context.Permissions
                .Where(x => x.IsActive)
                .OrderBy(x => x.ModuleName)
                .ThenBy(x => x.PermissionName)
                .ToListAsync();
        }

        public async Task<bool> ExistsByCodeAsync(
            string permissionCode)
        {
            return await _context.Permissions
                .AnyAsync(x =>
                    x.PermissionCode == permissionCode);
        }

        public async Task<bool> HasRolesAsync(
            int permissionId)
        {
            return await _context.RolePermissions
                .AnyAsync(x =>
                    x.PermissionId == permissionId);
        }
    }
}
