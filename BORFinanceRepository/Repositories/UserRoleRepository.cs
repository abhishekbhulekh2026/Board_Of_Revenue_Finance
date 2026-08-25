using SchoolDatabase.Context;
using BORFinanceDomain.Entities.Security;
using BORFinanceRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BORFinanceDTO.ViewModel;
using System.Security.Cryptography.X509Certificates;
namespace BORFinanceRepository.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly BORFinanceDbContext _context;

        public UserRoleRepository(BORFinanceDbContext context)
        {
            _context = context;
        }
        public async Task<UserRole?> GetUserRoleEntityAsync(
            long userId,
            int roleId)
        {
            return await _context.UserRoles
                .Include(x => x.User)
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x =>
                    x.UserId == userId &&
                    x.RoleId == roleId);
        }

        public async Task<UserRoleViewModel?> GetAsync(long userId, int roleId){
            return await _context.UserRoles
                .Where(x => x.UserId == userId && x.RoleId == roleId)
                .Select(x => new UserRoleViewModel
                {
                    UserId = x.UserId,
                    Name = x.User.FullName,

                    RoleId = x.RoleId,
                    RoleName = x.Role.RoleName,

                    AssignedAt = x.AssignedAt,
                    AssignedBy = x.AssignedBy,
                    RevokedAt = x.RevokedAt,
                    RevokedBy = x.RevokedBy,
                    IsActive = x.IsActive

                })
                .FirstOrDefaultAsync();
              
        }

        public async Task<IEnumerable<UserRoleViewModel>> GetAllAsync()
        {
            return await _context.UserRoles
                .Select(x => new UserRoleViewModel
                {
                    UserId = x.UserId,
                    Name = x.User.FullName,

                    RoleId = x.RoleId,
                    RoleName = x.Role.RoleName,

                    AssignedAt = x.AssignedAt,
                    AssignedBy = x.AssignedBy,
                    RevokedAt = x.RevokedAt,
                    RevokedBy = x.RevokedBy,
                    IsActive = x.IsActive

                }).OrderByDescending(x=>x.AssignedAt)
                .ToListAsync();

        }

        public async Task<IEnumerable<UserRoleViewModel>> GetByUserIdAsync(
            long userId)
        {
            return await _context.UserRoles
                .Where(x => x.UserId == userId)
                .Select(x => new UserRoleViewModel
                {
                    UserId = x.UserId,
                    Name = x.User.FullName,

                    RoleId = x.RoleId,
                    RoleName = x.Role.RoleName,

                    AssignedAt = x.AssignedAt,
                    AssignedBy = x.AssignedBy,
                    RevokedAt = x.RevokedAt,
                    RevokedBy = x.RevokedBy,
                    IsActive = x.IsActive

                })
                .ToListAsync();
        }

        public async Task<IEnumerable<UserRoleViewModel>> GetByRoleIdAsync(
            int roleId)
        {
            return await _context.UserRoles
              .Where(x => x.RoleId == roleId)
                .Select(x => new UserRoleViewModel
                {
                    UserId = x.UserId,
                    Name = x.User.FullName,

                    RoleId = x.RoleId,
                    RoleName = x.Role.RoleName,

                    AssignedAt = x.AssignedAt,
                    AssignedBy = x.AssignedBy,
                    RevokedAt = x.RevokedAt,
                    RevokedBy = x.RevokedBy,
                    IsActive = x.IsActive

                }).ToListAsync();
        }

        public async Task<bool> ExistsAsync(
            long userId,
            int roleId)
        {
            return await _context.UserRoles
                .AnyAsync(x =>
                    x.UserId == userId &&
                    x.RoleId == roleId);
        }

        public async Task<bool> UserExistsAsync(
           long userId)
        {
            return await _context.Users
                .AnyAsync(x =>
                    x.Id == userId);
        }

      
        public async Task<bool> RoleExistsAsync(
          int roleId)
        {
            return await _context.Roles
                .AnyAsync(x =>
                    x.RoleId == roleId);
        }

        public async Task<bool> HasRoleAsync(
            long userId,
            int roleId)
        {
            return await _context.UserRoles
                .AnyAsync(x =>
                    x.UserId == userId &&
                    x.RoleId == roleId &&
                    x.IsActive &&
                    !x.User.IsDeleted &&
                    x.Role.IsActive);
        }

        public async Task AddAsync(UserRole userRole)
        {
            await _context.UserRoles.AddAsync(userRole);
        }

        public void Update(UserRole userRole)
        {
            _context.UserRoles.Update(userRole);
        }

        public void Delete(UserRole userRole)
        {
            _context.UserRoles.Remove(userRole);
        }
    }
}
