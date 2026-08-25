using BORFinanceDomain.Entities.Employees;
using BORFinanceDTO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceRepository.Interfaces
{
    public interface IEmployeeRepository
     : IRepository<Employee, long>
    {
        Task<Employee?> GetByCodeAsync(
            string employeeCode);

        Task<bool> ExistsByCodeAsync(
            string employeeCode);

        Task<IEnumerable<Employee>>
            GetActiveEmployeesAsync();

        Task<EmployeeViewModel?> GetDetailsAsync(
            long employeeId);

        Task<IEnumerable<EmployeeViewModel>>
            GetAllDetailsAsync();
    }
}
