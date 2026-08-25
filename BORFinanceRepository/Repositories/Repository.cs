using BORFinanceDomain.Entities;
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
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
       where TEntity : class
    {
        protected readonly BORFinanceDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(BORFinanceDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            if (entity is ISoftDelete softDeleteEntity)
            {
                // Intercept hard delete and turn it into a soft delete update
                softDeleteEntity.IsDeleted = true;
                _context.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                // Fallback to physical deletion if the entity does not support soft delete
                _dbSet.Remove(entity);
            }
        }

        //public virtual async Task<bool> ExistsAsync(TKey id)
        //{
        //    return await _dbSet.FindAsync(id) != null;
        //}
        public virtual async Task<bool> ExistsAsync(TKey id)
        {
            // Find the primary key name defined in your EF configurations for this specific Entity
            var primaryKeyName = _context.Model
                .FindEntityType(typeof(TEntity))?
                .FindPrimaryKey()?
                .Properties
                .Select(p => p.Name)
                .FirstOrDefault();

            if (string.IsNullOrEmpty(primaryKeyName))
            {
                throw new InvalidOperationException($"No primary key defined for entity {typeof(TEntity).Name}");
            }

            // Passes the correct name (e.g., "RoleId", "UserId", or "Id") into the database query execution
            return await _dbSet.AnyAsync(e => EF.Property<TKey>(e, primaryKeyName).Equals(id));
        }

        //  Note: If your database primary keys use a name other than "Id"(such as "RoleId"), swap
        //  the string literal out or extract the primary key name dynamically using _context.Model.
        //  FindEntityType(typeof(TEntity)). Defaulting to "Id" works perfectly for standard conventions.
    }
}
