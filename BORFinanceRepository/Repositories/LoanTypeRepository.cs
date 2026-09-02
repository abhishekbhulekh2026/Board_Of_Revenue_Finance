using BORFinanceDomain.Loans;
using BORFinanceRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using SchoolDatabase.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceRepository.Repositories
{
    public class LoanTypeRepository
       : Repository<LoanType, int>,
         ILoanTypeRepository
    {
        public LoanTypeRepository(BORFinanceDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<LoanType>> GetAllAsync()
        {
            return await _context.LoanTypes
                .AsNoTracking()
                .OrderBy(x => x.LoanTypeName)
                .ToListAsync();
        }

        public async Task<LoanType?> GetByIdAsync(int id)
        {
            return await _context.LoanTypes
                .FirstOrDefaultAsync(x => x.LoanTypeId == id);
        }

        public async Task AddAsync(LoanType entity)
        {
            await _context.LoanTypes.AddAsync(entity);
        }

        public void Update(LoanType entity)
        {
            _context.LoanTypes.Update(entity);
        }

        public void Delete(LoanType entity)
        {
            // Since LoanType may be referenced by Loan,
            // actual deletion should be handled carefully.
            _context.LoanTypes.Remove(entity);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.LoanTypes
                .AnyAsync(x => x.LoanTypeId == id);
        }
    }
}
