using BORFinanceDomain.Entities.Security;
using BORFinanceDTO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceRepository.Interfaces
{
    public interface IUserRoleRepository
    {

        Task<UserRole?> GetUserRoleEntityAsync(long userId, int roleId);
        Task<UserRoleViewModel?> GetAsync(long userId, int roleId);

        Task<IEnumerable<UserRoleViewModel>> GetByUserIdAsync(long userId);

        Task<IEnumerable<UserRoleViewModel>> GetByRoleIdAsync(int roleId);

        Task<IEnumerable<UserRoleViewModel>> GetAllAsync();

        Task<bool> ExistsAsync(long userId, int roleId);

        Task<bool> HasRoleAsync(long userId, int roleId);

        Task AddAsync(UserRole userRole);

        void Update(UserRole userRole);

        void Delete(UserRole userRole);

        Task<bool> UserExistsAsync(
         long userId);
        Task<bool> RoleExistsAsync(
          int roleId);
    }
}
