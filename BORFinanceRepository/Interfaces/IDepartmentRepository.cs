using BORFinanceDomain.Entities.Employees;
using BORFinanceDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceRepository.Interfaces
{
    public interface IDepartmentRepository : IRepository<Department, int>
    {
        Task<IEnumerable<DropdownItemDto<int>>> GetDropDownAsync();
        Task AddDepartmentAsync(Department department);

    }
}
