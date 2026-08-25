using AutoMapper;
using Microsoft.Extensions.Logging;
using BORFinanceCommon.Exceptions;
using BORFinanceDomain.Entities.Security;
using BORFinanceDTO;
using BORFinanceDTO.ViewModel;
using BORFinanceRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolDatabase.Context;

namespace BORFinanceBusiness
{
    public interface IUserRoleService
    {
        Task<bool> CreateAsync(UserRoleDto userRoleDto);

        Task<UserRoleViewModel?> GetAsync(
            long userId,
            int roleId);

        Task<IEnumerable<UserRoleViewModel>> GetByUserIdAsync(
            long userId);

        Task<IEnumerable<UserRoleViewModel>> GetByRoleIdAsync(
            int roleId);

        Task<bool> UpdateAsync(UserRoleDto userRoleDto);

        Task<bool> DeleteAsync(
            long userId,
            int roleId);

        Task<IEnumerable<UserRoleViewModel>> GetAllAsync();
    }
    public class UserRoleService : IUserRoleService
    {
        private readonly BORFinanceDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<UserRoleService> _logger;
        private readonly IUserRoleRepository _userRoleRepository;
        public UserRoleService(
            BORFinanceDbContext context,
            IMapper mapper,
            ILogger<UserRoleService> logger,
            IUserRoleRepository userRoleRepository)
        {

            _context = context;
            _mapper = mapper;
            _logger = logger;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<bool> CreateAsync(UserRoleDto userRoleDto)
        {
            // Check whether user exists
            if (!await _userRoleRepository.UserExistsAsync(userRoleDto.UserId))
            {
                _logger.LogWarning(
                    "User not found: {UserId}",
                    userRoleDto.UserId);

                throw new BusinessException(
                    "User not found.");
            }

            // Check whether role exists
            if (!await _userRoleRepository.RoleExistsAsync(userRoleDto.RoleId))
            {
                _logger.LogWarning(
                    "Role not found: {RoleId}",
                    userRoleDto.RoleId);

                throw new BusinessException(
                    "Role not found.");
            }

            // Check duplicate assignment
            if (await _userRoleRepository.ExistsAsync(
                userRoleDto.UserId,
                userRoleDto.RoleId))
            {
                _logger.LogWarning(
                    "Role {RoleId} is already assigned to User {UserId}.",
                    userRoleDto.RoleId,
                    userRoleDto.UserId);

                throw new BusinessException(
                    "This role is already assigned to the user.");
            }

            var userRole = _mapper.Map<UserRole>(userRoleDto);

            userRole.AssignedAt = DateTime.UtcNow;
            userRole.IsActive = true;

            await _context.UserRoles.AddAsync(userRole);

            _logger.LogInformation(
                "Role {RoleId} assigned to User {UserId}.",
                userRole.RoleId,
                userRole.UserId);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<UserRoleViewModel?> GetAsync(
            long userId,
            int roleId)
        {
            var userRole = await _userRoleRepository
                .GetAsync(userId, roleId);

            return userRole;
        }

        public async Task<IEnumerable<UserRoleViewModel>> GetAllAsync()
        {
            var userRole = await _userRoleRepository
                .GetAllAsync();

            return userRole;
        }

        public async Task<IEnumerable<UserRoleViewModel>> GetByUserIdAsync(
            long userId)
        {
            var userRoles = await _userRoleRepository
                .GetByUserIdAsync(userId);

            return userRoles;
        }

        public async Task<IEnumerable<UserRoleViewModel>> GetByRoleIdAsync(
            int roleId)
        {
            var userRoles = await _userRoleRepository
                .GetByRoleIdAsync(roleId);

            return userRoles;
        }

        public async Task<bool> UpdateAsync(UserRoleDto userRoleDto)
        {
            var userRole = await _userRoleRepository
                .GetUserRoleEntityAsync(
                    userRoleDto.UserId,
                    userRoleDto.RoleId);

            if (userRole == null)
            {
                throw new BusinessException(
                    "User role assignment not found.");
            }

            // Don't overwrite the original assignment information
            userRole.AssignedBy = userRoleDto.AssignedBy;
            userRole.IsActive = userRoleDto.IsActive;

            if (!userRoleDto.IsActive)
            {
                userRole.RevokedAt = DateTime.UtcNow;
                userRole.RevokedBy = userRoleDto.RevokedBy;
            }
            else
            {
                userRole.RevokedAt = null;
                userRole.RevokedBy = null;
            }

            _context.UserRoles.Update(userRole);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(
            long userId,
            int roleId)
        {
            var userRole = await _userRoleRepository
                .GetUserRoleEntityAsync(userId, roleId);

            if (userRole == null)
            {
                throw new BusinessException(
                    "User role assignment not found.");
            }

           _userRoleRepository.Delete(userRole);

            _logger.LogInformation(
                "Role {RoleId} removed from User {UserId}.",
                roleId,
                userId);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}