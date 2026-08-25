using AutoMapper;
using BORFinanceCommon.Authentication;
using BORFinanceCommon.Exceptions;
using BORFinanceDomain.Entities.Security;
using BORFinanceDTO;
using BORFinanceRepository.Interfaces;
using BORFinanceRepository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SchoolDatabase.Context;

namespace BORFinanceBusiness
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllAsync();

        Task<RoleDto?> GetByIdAsync(int roleId);

        Task<RoleDto?> GetByCodeAsync(string roleCode);

        Task<IReadOnlyList<RoleDto>> GetActiveRolesAsync();

        Task<bool> CreateAsync(RoleDto roleDto);

        Task<bool> UpdateAsync(RoleDto roleDto);

        Task<bool> DeleteAsync(int roleId);

        Task<bool> ExistsByCodeAsync(string roleCode);

        Task<bool> DeactivateAsync(int roleId, int userId);

        Task<IEnumerable<RoleDto>> GetAllRolesAsync(int parentRoleId);

        Task<IEnumerable<UserDto>> GetUsersByRoleAsync(int roleId);
    }
    public class RoleService : IRoleService
    {

        private readonly IRoleRepository _roleRepository;
        private readonly BORFinanceDbContext _context;
        private readonly ILogger<RoleService> _logger;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IStoredProcedureRepository _spRepository;
        public RoleService(IRoleRepository roleRepository, BORFinanceDbContext context,
        ILogger<RoleService> logger, IMapper mapper, ICurrentUserService currentUserService, IStoredProcedureRepository spRepository)
        {
            _roleRepository = roleRepository;
            _context = context;
            _logger = logger;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _spRepository = spRepository;
        }

        public async Task<IEnumerable<RoleDto>> GetAllAsync()
        {
            var roles = await _roleRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<RoleDto>>(roles);
        }

        public async Task<RoleDto?> GetByIdAsync(int roleId)

        {
            var role = await _roleRepository.GetByIdAsync(roleId);
            return _mapper.Map<RoleDto>(role);
        }

        public async Task<RoleDto?> GetByCodeAsync(string roleCode)
        {
            var role = await _roleRepository.GetByCodeAsync(roleCode);
            return _mapper.Map<RoleDto>(role);
        }

        public async Task<IReadOnlyList<RoleDto>> GetActiveRolesAsync()
        {
            var roles = (await _roleRepository.GetActiveRolesAsync()).ToList();
            return _mapper.Map<IReadOnlyList<RoleDto>>(roles);
        }

        public async Task<bool> ExistsByCodeAsync(string roleCode)
        {
            return await _roleRepository.ExistsByCodeAsync(roleCode);
        }

        public async Task<bool> CreateAsync(RoleDto dto)
        {
            var userId = _currentUserService.UserId;

            if (userId == null)
                throw new BusinessException(
                    "User is not authenticated.");

            if (await _roleRepository
                .ExistsByCodeAsync(dto.RoleCode))
            {
                _logger.LogWarning(
                    "Duplicate role code: {RoleCode}",
                    dto.RoleCode);

                throw new BusinessException(
                    "Role code already exists.");
            }

            var role = _mapper.Map<Role>(dto);

            role.CreatedAt = DateTime.UtcNow;
            role.CreatedBy = userId;
            role.IsActive = true;

            await _roleRepository.AddAsync(role);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(RoleDto roleDto)
        {
            var role = _mapper.Map<Role>(roleDto);

            var existingRole = await _roleRepository.GetByIdAsync(role.RoleId);

            var updatedBy = _currentUserService.UserId;

            if (updatedBy == null)
            {
                throw new BusinessException("User is not authenticated.");
            }


            if (existingRole == null)
            {
                _logger.LogWarning("Role not exists: {RoleCode}", role.RoleCode);
                throw new BusinessException("Role codenot exists. ");
            }

            existingRole.RoleName = role.RoleName;
            existingRole.RoleDescription = role.RoleDescription;
            existingRole.RoleNameHi = role.RoleNameHi;
            existingRole.RoleDescriptionHi = role.RoleDescriptionHi;
            existingRole.SortOrder = role.SortOrder;
            existingRole.IsAssignable = role.IsAssignable;
            existingRole.IsActive = role.IsActive;
            existingRole.ParentRoleId = role.ParentRoleId;
            existingRole.UpdatedAt = DateTime.UtcNow;
            existingRole.UpdatedBy = updatedBy;

            _roleRepository.Update(existingRole);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int roleId)
        {
            var role = await _roleRepository.GetByIdAsync(roleId);

            if (role == null)
                return false;

            if (await _roleRepository.HasUsersAsync(roleId))
            {
                _logger.LogWarning("Cannot delete role: {RoleCode}", role.RoleCode);
                throw new BusinessException("Cannot delete role because users are assigned to it. ");
            }

            _roleRepository.Delete(role);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeactivateAsync(int roleId, int userId)
        {
            var role = await _roleRepository.GetByIdAsync(roleId);

            var deactivatedBy = _currentUserService.UserId;

            if (deactivatedBy == null)
            {
                throw new BusinessException("User is not authenticated.");
            }

            if (role == null)
                return false;

            if (await _roleRepository.HasUsersAsync(roleId))
            {
                _logger.LogWarning("Cannot be deactivated: {RoleCode}", role.RoleCode);
                throw new BusinessException("Role is assigned to users and cannot be deactivated. ");
            }

            role.IsActive = false;
            role.DeactivatedAt = DateTime.UtcNow;
            role.DeactivatedBy = deactivatedBy;

            _roleRepository.Update(role);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync(int parentRoleId)
        {
            var roles = await _spRepository.QueryAsync<Role>(
                "sp_GetRoles",
                new MySqlParameter("@ParentRoleId", parentRoleId));

            return _mapper.Map<IEnumerable<RoleDto>>(roles);
        }

        public async Task<IEnumerable<UserDto>> GetUsersByRoleAsync(int roleId)
        {
            return await _spRepository.QueryAsync<UserDto>(
                "sp_GetUsers",
                new MySqlParameter("@RoleId", roleId));
        }
    }
}
