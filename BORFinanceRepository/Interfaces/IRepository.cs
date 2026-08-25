using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceRepository.Interfaces
{
    public interface IRepository<TEntity, TKey>
     where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity?> GetByIdAsync(TKey id);

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        Task<bool> ExistsAsync(TKey id);
    }
}
