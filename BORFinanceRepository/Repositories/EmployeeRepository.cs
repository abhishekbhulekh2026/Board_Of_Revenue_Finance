using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolDatabase.Context;
using BORFinanceDomain.Entities.Employees;
using BORFinanceDTO.ViewModel;
using BORFinanceRepository.Interfaces;

namespace BORFinanceRepository.Repositories
{
  
    public class EmployeeRepository
        : Repository<Employee, long>,
          IEmployeeRepository
    {
        public EmployeeRepository(
            BORFinanceDbContext context)
            : base(context)
        {
        }

        public async Task<Employee?> GetByCodeAsync(
            string employeeCode)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(x =>
                    x.EmployeeCode == employeeCode);
        }

        public async Task<bool> ExistsByCodeAsync(
            string employeeCode)
        {
            return await _context.Employees
                .AnyAsync(x =>
                    x.EmployeeCode == employeeCode);
        }

        public async Task<IEnumerable<Employee>>
            GetActiveEmployeesAsync()
        {
            return await _context.Employees
                .Where(x => x.IsActive)
                .OrderBy(x => x.FullName)
                .ToListAsync();
        }

        public async Task<EmployeeViewModel?>
            GetDetailsAsync(long employeeId)
        {
            return await _context.Employees
                .Where(x => x.EmployeeId == employeeId)
                .Select(x => new EmployeeViewModel
                {
                    EmployeeId = x.EmployeeId,

                    UserId = x.UserId,

                    Username = x.User != null
                        ? x.User.Username
                        : null,

                    EmployeeCode = x.EmployeeCode,

                    FullName = x.FullName,

                    FullNameHi = x.FullNameHi,

                    FatherName = x.FatherName,

                    DateOfBirth = x.DateOfBirth,

                    MobileNumber = x.MobileNumber,

                    Email = x.Email,

                    Address = x.Address,

                    DateOfJoining = x.DateOfJoining,

                    BasicSalary = x.BasicSalary,

                    DepartmentId = x.DepartmentId,

                    DepartmentName = x.Department.DepartmentName,

                    DesignationId = x.DesignationId,

                    DesignationName = x.Designation.DesignationName,

                    EmployeeStatus = x.EmployeeStatus,

                    IsActive = x.IsActive,

                    CreatedAt = x.CreatedAt,

                    CreatedBy = x.CreatedBy,

                    UpdatedAt = x.UpdatedAt,

                    UpdatedBy = x.UpdatedBy
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<EmployeeViewModel>>
            GetAllDetailsAsync()
        {
            return await _context.Employees
                .Select(x => new EmployeeViewModel
                {
                    EmployeeId = x.EmployeeId,

                    UserId = x.UserId,

                    Username = x.User != null
                        ? x.User.Username
                        : null,

                    EmployeeCode = x.EmployeeCode,

                    FullName = x.FullName,

                    FullNameHi = x.FullNameHi,

                    FatherName = x.FatherName,

                    DateOfBirth = x.DateOfBirth,

                    MobileNumber = x.MobileNumber,

                    Email = x.Email,

                    Address = x.Address,

                    DateOfJoining = x.DateOfJoining,

                    BasicSalary = x.BasicSalary,

                    DepartmentId = x.DepartmentId,

                    DepartmentName = x.Department.DepartmentName,

                    DesignationId = x.DesignationId,

                    DesignationName = x.Designation.DesignationName,

                    EmployeeStatus = x.EmployeeStatus,

                    IsActive = x.IsActive,

                    CreatedAt = x.CreatedAt,

                    CreatedBy = x.CreatedBy,

                    UpdatedAt = x.UpdatedAt,

                    UpdatedBy = x.UpdatedBy
                })
                .OrderBy(x => x.FullName)
                .ToListAsync();
        }
    }
}
