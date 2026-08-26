using AutoMapper;
using BORFinanceCommon.Authentication;
using BORFinanceCommon.Exceptions;
using BORFinanceDomain.Members;
using BORFinanceDTO;
using BORFinanceRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolDatabase.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceBusiness
{

    public interface IMembershipService
    {
        Task<IEnumerable<MembershipDto>> GetAllAsync();

        Task<MembershipDto?> GetByIdAsync(long membershipId);

        Task<bool> CreateAsync(MembershipDto dto);

        Task<bool> UpdateAsync(MembershipDto dto);

        Task<bool> DeleteAsync(long membershipId);
    }

    public class MembershipService : IMembershipService
    {
        private readonly IMembershipRepository _membershipRepository;
        private readonly BORFinanceDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly ILogger<MembershipService> _logger;

        public MembershipService(
            IMembershipRepository membershipRepository,
            BORFinanceDbContext context,
            ICurrentUserService currentUserService,
            IMapper mapper,
            ILogger<MembershipService> logger)
        {
            _membershipRepository = membershipRepository;
            _context = context;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<MembershipDto>> GetAllAsync()
        {
            var memberships =
                await _membershipRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<MembershipDto>>(memberships);
        }

        public async Task<MembershipDto?> GetByIdAsync(long membershipId)
        {
            var membership =
                await _membershipRepository.GetByIdAsync(membershipId);

            if (membership == null)
                return null;

            return _mapper.Map<MembershipDto>(membership);
        }

        public async Task<bool> CreateAsync(MembershipDto dto)
        {
            var employeeExists = await _context.Employees
                .AnyAsync(x =>
                    x.EmployeeId == dto.EmployeeId &&
                    x.IsActive);

            if (!employeeExists)
                throw new BusinessException(
                    "Employee not found or inactive.");

            if (await _membershipRepository
                .ExistsByEmployeeIdAsync(dto.EmployeeId))
            {
                throw new BusinessException(
                    "Membership already exists for this employee.");
            }

            if (await _membershipRepository
                .ExistsByMembershipNumberAsync(
                    dto.MembershipNumber))
            {
                throw new BusinessException(
                    "Membership number already exists.");
            }

            var membership =
                _mapper.Map<Membership>(dto);

            membership.MembershipId = 0;
            membership.CreatedAt = DateTime.UtcNow;
            membership.CreatedBy = _currentUserService.UserId;
            membership.Status = "Active";

            await _membershipRepository.AddAsync(
                membership);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(MembershipDto dto)
        {
            var membership =
                await _membershipRepository.GetByIdAsync(
                    dto.MembershipId);

            if (membership == null)
            {
                throw new BusinessException(
                    "Membership not found.");
            }

            // Employee must exist
            var employeeExists =
                await _context.Employees
                    .AnyAsync(x =>
                        x.EmployeeId == dto.EmployeeId &&
                        x.IsActive);

            if (!employeeExists)
            {
                throw new BusinessException(
                    "Employee not found or inactive.");
            }

            // Check duplicate membership number
            var duplicateNumber =
                await _context.Memberships
                    .AnyAsync(x =>
                        x.MembershipNumber ==
                            dto.MembershipNumber &&
                        x.MembershipId !=
                            dto.MembershipId);

            if (duplicateNumber)
            {
                throw new BusinessException(
                    "Membership number already exists.");
            }

            // Because Employee → Membership is 1:1
            var duplicateEmployee =
                await _context.Memberships
                    .AnyAsync(x =>
                        x.EmployeeId == dto.EmployeeId &&
                        x.MembershipId !=
                            dto.MembershipId);

            if (duplicateEmployee)
            {
                throw new BusinessException(
                    "Another membership already exists for this employee.");
            }

            membership.EmployeeId = dto.EmployeeId;
            membership.MembershipNumber =
                dto.MembershipNumber;

            membership.MembershipDate =
                dto.MembershipDate;

            membership.ShareCount =
                dto.ShareCount;

            membership.ShareValue =
                dto.ShareValue;

            membership.Status =
                dto.Status;

            membership.UpdatedAt = DateTime.UtcNow;
            membership.UpdatedBy =
                _currentUserService.UserId;

            _membershipRepository.Update(membership);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(long membershipId)
        {
            var membership =
                await _membershipRepository.GetByIdAsync(
                    membershipId);

            if (membership == null)
            {
                throw new BusinessException(
                    "Membership not found.");
            }

            // Recommended: don't delete a membership
            // if financial facilities exist.

            var hasLoans =
                await _context.Loans
                    .AnyAsync(x =>
                        x.MembershipId ==
                        membershipId);

            var hasFixedDeposits =
                await _context.FixedDeposits
                    .AnyAsync(x =>
                        x.MembershipId ==
                        membershipId);

            if (hasLoans || hasFixedDeposits)
            {
                throw new BusinessException(
                    "Membership cannot be deleted because financial facilities are linked to it.");
            }

            _membershipRepository.Delete(membership);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
