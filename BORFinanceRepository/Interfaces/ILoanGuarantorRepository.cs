using BORFinanceDomain.Loans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceRepository.Interfaces
{
    public interface ILoanGuarantorRepository
     : IRepository<LoanGuarantor, long>
    {
        Task<IEnumerable<LoanGuarantor>> GetAllAsync();

        Task<LoanGuarantor?> GetByIdAsync(long id);

        Task AddAsync(LoanGuarantor entity);

        void Update(LoanGuarantor entity);

        void Delete(LoanGuarantor entity);

        Task<bool> ExistsAsync(long id);
    }
}
