using MemeSite.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MemeSite.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _applicationDbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _applicationDbContext = context;
            _dbSet = _applicationDbContext.Set<TEntity>();
        }
        public async Task<TEntity> FindAsync(params object[] keyValues) => await _dbSet.FindAsync(keyValues);
        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter) => await _dbSet.FirstOrDefaultAsync(filter);

        public async Task<List<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task DeleteAsync(params object[] keyValues)
        {
            var entity = await FindAsync(keyValues);
            await DeleteAsync(entity);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _applicationDbContext.SaveChangesAsync();
        }
        public async Task InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _applicationDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(TEntity entity)
        {
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter) => await _dbSet.CountAsync(filter);
        public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> filter) => await CountAsync(filter) > 0;
    }
}
