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
    public class LoanGuarantorRepository : Repository<LoanGuarantor, long>, ILoanGuarantorRepository
    {
        public LoanGuarantorRepository(BORFinanceDbContext context) : base(context)
        { }
            public async Task<IEnumerable<LoanGuarantor>> GetAllAsync()
        {
            return await _context.LoanGuarantors
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<LoanGuarantor?> GetByIdAsync(long id)
        {
            return await _context.LoanGuarantors
                .FirstOrDefaultAsync(x =>
                    x.LoanGuarantorId == id);
        }

        public async Task AddAsync(LoanGuarantor entity)
        {
            await _context.LoanGuarantors.AddAsync(entity);
        }

        public void Update(LoanGuarantor entity)
        {
            _context.LoanGuarantors.Update(entity);
        }

        public void Delete(LoanGuarantor entity)
        {
            _context.LoanGuarantors.Remove(entity);
        }

        public async Task<bool> ExistsAsync(long id)
        {
            return await _context.LoanGuarantors
                .AnyAsync(x =>
                    x.LoanGuarantorId == id);
        }
    }
}
