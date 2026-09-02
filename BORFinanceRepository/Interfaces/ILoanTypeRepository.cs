using BORFinanceDomain.Loans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceRepository.Interfaces
{
    public interface ILoanTypeRepository
       : IRepository<LoanType, int>
    {
        Task<IEnumerable<LoanType>> GetAllAsync();

        Task<LoanType?> GetByIdAsync(int id);

        Task AddAsync(LoanType entity);

        void Update(LoanType entity);

        void Delete(LoanType entity);

        Task<bool> ExistsAsync(int id);
    }
}
