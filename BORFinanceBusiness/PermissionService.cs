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
    public interface IPermissionService
    {
        Task<bool> CreateAsync(
            PermissionDto dto);

        Task<IEnumerable<PermissionDto>>
            GetAllAsync();

        Task<PermissionDto?> GetByIdAsync(
            int permissionId);

        Task<IEnumerable<PermissionDto>>
            GetActiveAsync();

        Task<bool> UpdateAsync(
            PermissionDto dto);

        Task<bool> DeleteAsync(
            int permissionId);
    }

    public class PermissionService
    : IPermissionService
    {
        private readonly BORFinanceDbContext _context;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PermissionService> _logger;

        public PermissionService(
            BORFinanceDbContext context, IPermissionRepository permissionRepository,
            IMapper mapper,
            ILogger<PermissionService> logger)

        {
            _permissionRepository= permissionRepository;
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> CreateAsync(
            PermissionDto dto)
        {
            if (await _permissionRepository
                .ExistsByCodeAsync(dto.PermissionCode))
            {
                _logger.LogWarning(
                    "Duplicate permission code: {PermissionCode}",
                    dto.PermissionCode);

                throw new BusinessException(
                    "Permission code already exists.");
            }

            var permission =
                _mapper.Map<Permission>(dto);

            permission.CreatedAt =
                DateTime.UtcNow;

            permission.IsActive = true;

            await _permissionRepository
                .AddAsync(permission);

            _logger.LogInformation(
                "Permission created: {PermissionCode}",
                permission.PermissionCode);

            return await _context
                .SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<PermissionDto>>
            GetAllAsync()
        {
            var permissions =
                await _permissionRepository
                    .GetAllAsync();

            return _mapper.Map<IEnumerable<PermissionDto>>(
                permissions);
        }

        public async Task<PermissionDto?>
            GetByIdAsync(int permissionId)
        {
            var permission =
                await _permissionRepository
                    .GetByIdAsync(permissionId);

            return _mapper.Map<PermissionDto>(
                permission);
        }

        public async Task<IEnumerable<PermissionDto>>
            GetActiveAsync()
        {
            var permissions =
                await _permissionRepository
                    .GetActivePermissionsAsync();

            return _mapper.Map<IEnumerable<PermissionDto>>(
                permissions);
        }

        public async Task<bool> UpdateAsync(
            PermissionDto dto)
        {
            var permission =
                await _permissionRepository
                    .GetByIdAsync(dto.PermissionId);

            if (permission == null)
            {
                throw new BusinessException(
                    "Permission not found.");
            }

            // Check duplicate code
            if (!string.Equals(
                    permission.PermissionCode,
                    dto.PermissionCode,
                    StringComparison.OrdinalIgnoreCase))
            {
                if (await _permissionRepository
                    .ExistsByCodeAsync(dto.PermissionCode))
                {
                    _logger.LogWarning(
                        "Duplicate permission code during update: {PermissionCode}",
                        dto.PermissionCode);

                    throw new BusinessException(
                        "Permission code already exists.");
                }
            }

            permission.PermissionCode =
                dto.PermissionCode;

            permission.PermissionName =
                dto.PermissionName;

            permission.ModuleName =
                dto.ModuleName;

            permission.Description =
                dto.Description;

            permission.IsActive =
                dto.IsActive;

            _permissionRepository
                .Update(permission);

            _logger.LogInformation(
                "Permission updated: {PermissionId}",
                dto.PermissionId);

            return await _context
                .SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(
            int permissionId)
        {
            var permission =
                await _permissionRepository
                    .GetByIdAsync(permissionId);

            if (permission == null)
            {
                throw new BusinessException(
                    "Permission not found.");
            }

            // Don't physically delete if roles use it
            if (await _permissionRepository
                .HasRolesAsync(permissionId))
            {
                throw new BusinessException(
                    "Permission cannot be deleted because it is assigned to one or more roles.");
            }

            _permissionRepository
                .Delete(permission);

            _logger.LogInformation(
                "Permission deleted: {PermissionId}",
                permissionId);

            return await _context
                .SaveChangesAsync() > 0;
        }
    }
}
