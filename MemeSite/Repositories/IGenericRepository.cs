using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MemeSite.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> FindAsync(params object[] keyValues);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter);
        Task<List<TEntity>> GetAllAsync();
        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(params object[] keyValues);
        Task InsertAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter);
        Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> filter);
    }
}
