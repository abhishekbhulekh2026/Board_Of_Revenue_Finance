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
    public class RolePermissionRepository
        : IRolePermissionRepository
    {
        private readonly BORFinanceDbContext _context;

        public RolePermissionRepository(
            BORFinanceDbContext context)
        {
            _context = context;
        }

        public async Task<RolePermission?> GetAsync(
            int roleId,
            int permissionId)
        {
            return await _context.RolePermissions
                .Include(x => x.Role)
                .Include(x => x.Permission)
                .FirstOrDefaultAsync(x =>
                    x.RoleId == roleId &&
                    x.PermissionId == permissionId);
        }

        public async Task<IEnumerable<RolePermission>>
            GetByRoleIdAsync(int roleId)
        {
            return await _context.RolePermissions
                .Include(x => x.Permission)
                .Where(x => x.RoleId == roleId)
                .OrderBy(x => x.PermissionId)
                .ToListAsync();
        }

        public async Task<IEnumerable<RolePermission>>
            GetByPermissionIdAsync(int permissionId)
        {
            return await _context.RolePermissions
                .Include(x => x.Role)
                .Where(x => x.PermissionId == permissionId)
                .OrderBy(x => x.RoleId)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(
            int roleId,
            int permissionId)
        {
            return await _context.RolePermissions
                .AnyAsync(x =>
                    x.RoleId == roleId &&
                    x.PermissionId == permissionId);
        }
       


        public async Task<bool> RoleExistsAsync(
           int roleId)
        {
            return await _context.Roles
                .AnyAsync(x =>
                    x.RoleId == roleId);
        }
        public async Task<bool> PermissionExistsAsync(
           int permissionId)
        {
            return await _context.Permissions
                .AnyAsync(x =>
                    x.PermissionId == permissionId);
        }

        public async Task<bool> IsAllowedAsync(
            int roleId,
            int permissionId)
        {
            return await _context.RolePermissions
                .AnyAsync(x =>
                    x.RoleId == roleId &&
                    x.PermissionId == permissionId &&
                    x.IsAllowed);
        }

        public async Task AddAsync(
            RolePermission rolePermission)
        {
            await _context.RolePermissions
                .AddAsync(rolePermission);
        }

        public void Update(
            RolePermission rolePermission)
        {
            _context.RolePermissions.Update(rolePermission);
        }

        public void Delete(
            RolePermission rolePermission)
        {
            _context.RolePermissions.Remove(rolePermission);
        }
    }
}