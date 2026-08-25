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
    public class DesignationRepository : Repository<Designation, int> , IDesignationRepository
    {
        public DesignationRepository(BORFinanceDbContext context):base(context)
        {}
        public async Task<IEnumerable<DropdownItemDto<int>>> GetDesignationAsync()
        {
            return await _context.Designations.Select(d => new DropdownItemDto<int>
            {
                Id = d.DesignationId,
                Name = d.DesignationName
            }).ToListAsync();
        }
    }
}
