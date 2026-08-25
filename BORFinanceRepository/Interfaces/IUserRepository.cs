using BORFinanceDomain.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceRepository.Interfaces
{
    public interface IUserRepository : IRepository<User, long>
    {
        Task<User?> GetByUsernameAsync(string username);

        //Task<User?> GetByEmailAsync(string email);

        Task<bool> UsernameExistsAsync(string username);

        Task<bool> EmailExistsAsync(string email);

        Task<User?> GetUserForLoginAsync(string username);

        Task<UserRole?> GetUserRolesByUserId(string username);

        //  Task UpdateLastLoginAsync(long userId);

        //  Task IncrementFailedLoginAsync(long userId);

        // Task ResetFailedLoginAsync(long userId);

        // Task LockAccountAsync(long userId);

        // Task<IEnumerable<User>> GetPendingApprovalUsersAsync();
    }
}
