using Microsoft.EntityFrameworkCore;
using SchoolDatabase.Context;
using BORFinanceDomain.Entities.Employees;
using BORFinanceDTO;
using BORFinanceRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceRepository.Repositories
{
    public  class DepartmentRepository : Repository<Department, int>, IDepartmentRepository
    {
     
        public DepartmentRepository(BORFinanceDbContext context)
            :base(context)
        {
        }
        public async Task<IEnumerable<DropdownItemDto<int>>> GetDropDownAsync()
        {
            return await _context.Departments.Select(d => new DropdownItemDto<int>
            {
                Id = d.DepartmentId,
                Name = d.DepartmentName
            }).ToListAsync();
        }

        public async Task AddDepartmentAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
        }
    }
}
