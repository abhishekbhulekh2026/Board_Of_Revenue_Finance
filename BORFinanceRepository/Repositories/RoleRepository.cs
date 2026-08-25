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
    public class RoleRepository : Repository<Role, int>, IRoleRepository
    {
        

        public RoleRepository(BORFinanceDbContext context)
            : base(context)
        {
           
        }
        public async Task<Role?> GetByCodeAsync(string roleCode)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(x => x.RoleCode == roleCode);
        }

        public async Task<IEnumerable<Role>> GetActiveRolesAsync()
        {
            return await _context.Roles
                .Where(x => x.IsActive)
                .OrderBy(x => x.SortOrder)
                .ToListAsync();
        }

        public async Task<bool> ExistsByCodeAsync(string roleCode)
        {
            return await _context.Roles
                .AnyAsync(x => x.RoleCode == roleCode);
        }

        public async Task<bool> HasUsersAsync(int roleId)
        {
            // return await _context.Users
            //  .AnyAsync(x => x.RoleId == roleId && !x.IsDeleted);

            return await

           _context.UserRoles.AnyAsync(x => x.RoleId == roleId && !x.User.IsDeleted);
        }
    }
}
