using Microsoft.EntityFrameworkCore;
using SchoolDatabase.Context;
using BORFinanceDomain.Entities.Security;
using BORFinanceRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceRepository.Repositories
{
    public class UserRepository
     : Repository<User, long>, IUserRepository
    {
      
        public UserRepository(BORFinanceDbContext context)
            : base(context)
        {
           
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(x => x.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(x =>
                    x.Username == username &&
                    !x.IsDeleted);
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _context.Users
                .AnyAsync(x => x.Username == username);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users
                .AnyAsync(x => x.Email == email);
        }

        public async Task<bool>AuthenticateUserAsync(string username, string password)
        {
            return await _context.Users
                .AnyAsync(x => x.Username == username && x.PasswordHash == password);
        }

        public async Task<User?> GetUserForLoginAsync(string username)
        {
            return await _context.Users
                .Include(x => x.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(x =>
                    x.Username == username &&
                    !x.IsDeleted);
        }

        public async Task<UserRole?> GetUserRolesByUserId(string username)
        {
            return await _context.UserRoles
                .Include(ur => ur.User) // Include user to safely filter by username
                .Include(ur => ur.Role)
                .FirstOrDefaultAsync(ur => ur.User.Username == username);
        }
    }
}
