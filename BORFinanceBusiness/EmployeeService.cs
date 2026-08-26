using AutoMapper;
using Microsoft.Extensions.Logging;
using BORFinanceCommon.Authentication;
using BORFinanceCommon.Exceptions;
using BORFinanceDomain.Entities.Employees;
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
    public interface IEmployeeService
    {
        Task<bool> CreateAsync(EmployeeDto dto);

        Task<EmployeeDto?> GetByIdAsync(
            long employeeId);

        Task<IEnumerable<EmployeeDto>>
            GetAllAsync();

        Task<IEnumerable<EmployeeDto>>
            GetActiveAsync();

        Task<EmployeeViewModel?>
            GetDetailsAsync(long employeeId);

        Task<IEnumerable<EmployeeViewModel>>
            GetAllDetailsAsync();

        Task<bool> UpdateAsync(EmployeeDto dto);

        Task<bool> DeleteAsync(long employeeId);
    }

    public class EmployeeService
    : IEmployeeService
    {
        private readonly BORFinanceDbContext _context;
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;
        private readonly ICurrentUserService _currentUserService;

        public EmployeeService(
            BORFinanceDbContext context,
            IMapper mapper,
            ILogger<EmployeeService> logger,
            ICurrentUserService currentUserService, IEmployeeRepository employeeRepository)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _currentUserService = currentUserService;

            _employeeRepository = employeeRepository;
        }

        public async Task<bool> CreateAsync(
            EmployeeDto dto)
        {
            var userId = _currentUserService.UserId;

            if (userId == null)
            {
                throw new BusinessException(
                    "User is not authenticated.");
            }

            // Employee code duplicate check
            if (await _employeeRepository
                .ExistsByCodeAsync(dto.EmployeeCode))
            {
                _logger.LogWarning(
                    "Duplicate employee code: {EmployeeCode}",
                    dto.EmployeeCode);

                throw new BusinessException(
                    "Employee code already exists.");
            }

            // Department validation
            //if (!await _employeeRepository
            //    .ExistsAsync(dto.DepartmentId))
            //{
            //    throw new BusinessException(
            //        "Department not found.");
            //}

            // Designation validation
            //if (!await _context.Designations
            //    .ExistsAsync(dto.DesignationId))
            //{
            //    throw new BusinessException(
            //        "Designation not found.");
            //}

            // Optional User validation
            //if (dto.UserId.HasValue)
            //{
            //    if (!await _employeeRepository
            //        .ExistsAsync(dto.UserId.Value))
            //    {
            //        throw new BusinessException(
            //            "User not found.");
            //    }
            //}

            var employee =
                _mapper.Map<Employee>(dto);

            employee.CreatedAt =
                DateTime.UtcNow;

            employee.CreatedBy =
                userId;

            employee.IsActive = true;

            await _employeeRepository
                .AddAsync(employee);

            _logger.LogInformation(
                "Employee created: {EmployeeCode} by User {UserId}",
                employee.EmployeeCode,
                userId);

            return await _context
                .SaveChangesAsync() > 0;
        }

        public async Task<EmployeeDto?>
            GetByIdAsync(long employeeId)
        {
            var employee =
                await _employeeRepository
                    .GetByIdAsync(employeeId);

            return _mapper.Map<EmployeeDto>(
                employee);
        }

        public async Task<IEnumerable<EmployeeDto>>
            GetAllAsync()
        {
            var employees =
                await _employeeRepository
                    .GetAllAsync();

            return _mapper.Map<IEnumerable<EmployeeDto>>(
                employees);
        }

        public async Task<IEnumerable<EmployeeDto>>
            GetActiveAsync()
        {
            var employees =
                await _employeeRepository
                    .GetActiveEmployeesAsync();

            return _mapper.Map<IEnumerable<EmployeeDto>>(
                employees);
        }

        public async Task<EmployeeViewModel?>
            GetDetailsAsync(long employeeId)
        {
            return await _employeeRepository
                .GetDetailsAsync(employeeId);
        }

        public async Task<IEnumerable<EmployeeViewModel>>
            GetAllDetailsAsync()
        {
            return await _employeeRepository
                .GetAllDetailsAsync();
        }

        public async Task<bool> UpdateAsync(
            EmployeeDto dto)
        {
            var userId = _currentUserService.UserId;

            if (userId == null)
            {
                throw new BusinessException(
                    "User is not authenticated.");
            }

            var employee =
                await _employeeRepository
                    .GetByIdAsync(dto.EmployeeId);

            if (employee == null)
            {
                throw new BusinessException(
                    "Employee not found.");
            }

            // Employee code duplicate check
            if (!string.Equals(
                    employee.EmployeeCode,
                    dto.EmployeeCode,
                    StringComparison.OrdinalIgnoreCase))
            {
                if (await _employeeRepository
                    .ExistsByCodeAsync(dto.EmployeeCode))
                {
                    throw new BusinessException(
                        "Employee code already exists.");
                }
            }

            // Department validation
            if (!await _employeeRepository
                .ExistsAsync(dto.DepartmentId))
            {
                throw new BusinessException(
                    "Department not found.");
            }

            //Designation validation
            if (!await _employeeRepository
                .ExistsAsync(dto.DesignationId))
            {
                throw new BusinessException(
                    "Designation not found.");
            }

            // User validation
            if (dto.UserId.HasValue)
            {
                if (!await _employeeRepository
                    .ExistsAsync(dto.UserId.Value))
                {
                    throw new BusinessException(
                        "User not found.");
                }
            }

            employee.UserId = dto.UserId;

            employee.EmployeeCode =
                dto.EmployeeCode;

            employee.FullName =
                dto.FullName;

            employee.FullNameHi =
                dto.FullNameHi;

            employee.FatherName =
                dto.FatherName;

            employee.DateOfBirth =
                dto.DateOfBirth;

            employee.MobileNumber =
                dto.MobileNumber;

            employee.Email =
                dto.Email;

            employee.Address =
                dto.Address;

            employee.DateOfJoining =
                dto.DateOfJoining;

            employee.BasicSalary =
                dto.BasicSalary;

            employee.DepartmentId =
                dto.DepartmentId;

            employee.DesignationId =
                dto.DesignationId;

            employee.EmployeeStatus =
                dto.EmployeeStatus;

            employee.IsActive =
                dto.IsActive;

            employee.UpdatedAt =
                DateTime.UtcNow;

            employee.UpdatedBy =
                userId;

            _employeeRepository
                .Update(employee);

            _logger.LogInformation(
                "Employee updated: {EmployeeId} by User {UserId}",
                employee.EmployeeId,
                userId);

            return await _context
                .SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(
            long employeeId)
        {
            var userId = _currentUserService.UserId;

            if (userId == null)
            {
                throw new BusinessException(
                    "User is not authenticated.");
            }

            var employee =
                await _employeeRepository
                    .GetByIdAsync(employeeId);

            if (employee == null)
            {
                throw new BusinessException(
                    "Employee not found.");
            }

            _employeeRepository
                .Delete(employee);

            _logger.LogInformation(
                "Employee deleted: {EmployeeId} by User {UserId}",
                employeeId,
                userId);

            return await _context
                .SaveChangesAsync() > 0;
        }
    }
}
