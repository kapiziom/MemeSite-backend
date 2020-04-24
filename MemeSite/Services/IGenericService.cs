using MemeSite.Data.Models.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MemeSite.Services
{
    public interface IGenericService<TEntity> where TEntity : class
    {
        //async
        Task<TEntity> FindAsync(params object[] keyValues);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter);
        Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> filter);
        Task<Result<TEntity>> Insert(TEntity entity);
        Task<Result<TEntity>> Update(TEntity entity);
        Task<List<TEntity>> GetAllFilteredIncludeAsync(Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, object>>[] includeExpressions);
    }
}
