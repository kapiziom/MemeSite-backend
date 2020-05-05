using MemeSite.Data.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MemeSite.Data.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Query();
        Task<List<TEntity>> GetAllFilteredIncludeAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includeExpressions);

        TEntity Find(params object[] keyValues);
        Task<TEntity> FindAsync(params object[] keyValues);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter);
        //get all
        Task<List<TEntity>> GetAllAsync();
        //get all with filter
        Task<List<TEntity>> GetAllFilteredAsync(Expression<Func<TEntity, bool>> filter);
        //get all with filter and OrderBy   
        Task<List<TEntity>> GetAllFilteredAsync<TKey>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> orderByDescending);

        //paged lists
        Task<PagedList<TEntity>> GetPagedAsync<TKey>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> order, int page, int itemsPerPage);
        //paged list with includes.
        Task<PagedList<TEntity>> GetPagedAsync<TKey>(Expression<Func<TEntity, bool>> filter, 
            Expression<Func<TEntity, TKey>> order, int page, int itemsPerPage, 
            params Expression<Func<TEntity, object>>[] includeExpressions);


        ///////////////
        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(params object[] keyValues);
        Task<TEntity> InsertAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter);
        Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> filter);

        TEntity Insert(TEntity entity);

        bool IsUnique(TEntity entity);
    }
}
