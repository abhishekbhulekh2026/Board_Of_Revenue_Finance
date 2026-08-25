using AutoMapper;
using Microsoft.Extensions.Logging;
using BORFinanceCommon.Exceptions;
using BORFinanceDomain.Entities.Security;
using BORFinanceDTO;
using BORFinanceRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolDatabase.Context;

namespace BORFinanceBusiness
{

    public interface IRolePermissionService
    {
        Task<bool> CreateAsync(
            RolePermissionDto dto);

        Task<RolePermissionDto?> GetAsync(
            int roleId,
            int permissionId);

        Task<IEnumerable<RolePermissionDto>>
            GetByRoleIdAsync(int roleId);

        Task<IEnumerable<RolePermissionDto>>
            GetByPermissionIdAsync(int permissionId);

        Task<bool> UpdateAsync(
            RolePermissionDto dto);

        Task<bool> DeleteAsync(
            int roleId,
            int permissionId);
    }
    public class RolePermissionService
      : IRolePermissionService
    {
        private readonly BORFinanceDbContext _context;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RolePermissionService> _logger;

        public RolePermissionService(
            BORFinanceDbContext context,
            IMapper mapper,
            ILogger<RolePermissionService> logger,
            IRolePermissionRepository rolePermissionRepository)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _rolePermissionRepository = rolePermissionRepository;
        }

        public async Task<bool> CreateAsync(
            RolePermissionDto dto)
        {
            // Check Role
            if (!await _rolePermissionRepository
                .RoleExistsAsync(dto.RoleId))
            {
                _logger.LogWarning(
                    "Role not found: {RoleId}",
                    dto.RoleId);

                throw new BusinessException(
                    "Role not found.");
            }

            // Check Permission
            if (!await _rolePermissionRepository
                .PermissionExistsAsync(dto.PermissionId))
            {
                _logger.LogWarning(
                    "Permission not found: {PermissionId}",
                    dto.PermissionId);

                throw new BusinessException(
                    "Permission not found.");
            }

            // Check duplicate mapping
            if (await _rolePermissionRepository
                .ExistsAsync(
                    dto.RoleId,
                    dto.PermissionId))
            {
                _logger.LogWarning(
                    "Permission {PermissionId} is already assigned to Role {RoleId}.",
                    dto.PermissionId,
                    dto.RoleId);

                throw new BusinessException(
                    "This permission is already assigned to the role.");
            }

            var rolePermission =
                _mapper.Map<RolePermission>(dto);

            rolePermission.AssignedAt =
                DateTime.UtcNow;

            rolePermission.IsAllowed = true;

            await _context.RolePermissions
                .AddAsync(rolePermission);

            _logger.LogInformation(
                "Permission {PermissionId} assigned to Role {RoleId}.",
                dto.PermissionId,
                dto.RoleId);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<RolePermissionDto?> GetAsync(
            int roleId,
            int permissionId)
        {
            var entity = await _rolePermissionRepository
                .GetAsync(roleId, permissionId);

            return _mapper.Map<RolePermissionDto>(
                entity);
        }

        public async Task<IEnumerable<RolePermissionDto>>
            GetByRoleIdAsync(int roleId)
        {
            var entities = await _rolePermissionRepository
                .GetByRoleIdAsync(roleId);

            return _mapper.Map<IEnumerable<RolePermissionDto>>(
                entities);
        }

        public async Task<IEnumerable<RolePermissionDto>>
            GetByPermissionIdAsync(int permissionId)
        {
            var entities = await _rolePermissionRepository
                .GetByPermissionIdAsync(permissionId);

            return _mapper.Map<IEnumerable<RolePermissionDto>>(     
                entities);
        }

        public async Task<bool> UpdateAsync(
            RolePermissionDto dto)
        {
            var entity = await _rolePermissionRepository
                .GetAsync(
                    dto.RoleId,
                    dto.PermissionId);

            if (entity == null)
            {
                throw new BusinessException(
                    "Role permission assignment not found.");
            }

            entity.IsAllowed = dto.IsAllowed;

            _context.RolePermissions.Update(entity);

            _logger.LogInformation(
                "Permission {PermissionId} updated for Role {RoleId}. Allowed: {IsAllowed}",
                dto.PermissionId,
                dto.RoleId,
                dto.IsAllowed);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(
            int roleId,
            int permissionId)
        {
            var entity = await _rolePermissionRepository
                .GetAsync(
                    roleId,
                    permissionId);

            if (entity == null)
            {
                throw new BusinessException(
                    "Role permission assignment not found.");
            }

            _rolePermissionRepository.Delete(entity);

            _logger.LogInformation(
                "Permission {PermissionId} removed from Role {RoleId}.",
                permissionId,
                roleId);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
