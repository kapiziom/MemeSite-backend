using MemeSite.Data;
using MemeSite.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MemeSite.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _applicationDbContext = context;
            _dbSet = _applicationDbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> Query() => _dbSet;
        public async Task<TEntity> FindAsync(params object[] keyValues) => await _dbSet.FindAsync(keyValues);
        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter) => await _dbSet.FirstOrDefaultAsync(filter);

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> include) =>
            await _dbSet.Include(include).FirstOrDefaultAsync(filter);

        public async Task<List<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task<List<TEntity>> GetAllFilteredAsync(Expression<Func<TEntity, bool>> filter) => 
            await _dbSet.Where(filter).ToListAsync();

        public virtual async Task<List<TEntity>> GetAllFilteredAsync<TKey>(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TKey>> orderByDescending) => await _dbSet.Where(filter).OrderByDescending(orderByDescending).ToListAsync();


        public virtual async Task<PagedList<TEntity>> GetPagedAsync<TKey>(
           Expression<Func<TEntity, bool>> filter,
           Expression<Func<TEntity, TKey>> orderByDescending,
           int page,
           int itemsPerPage)
        {
            var skip = (page - 1) * itemsPerPage;
            var query = _dbSet.AsQueryable();

            query = query.Where(filter);
            var total = await query.CountAsync();
            int pageCount = (int)Math.Ceiling(((double)total / itemsPerPage));
            var result = await query
                .OrderByDescending(orderByDescending)
                .Skip(skip)
                .Take(itemsPerPage)
                .ToListAsync();

            return new PagedList<TEntity>()
            {
                Page = page,
                ItemsPerPage = itemsPerPage,
                PageCount = pageCount,
                TotalItems = total,
                Items = result
            };
        }

        public virtual async Task<PagedList<TEntity>> GetPagedAsync2<TKey>(
           Expression<Func<TEntity, bool>> filter,
           Expression<Func<TEntity, TKey>> orderByDescending,
           int page,
           int itemsPerPage)
        {
            var skip = (page - 1) * itemsPerPage;
            var query = _dbSet.AsQueryable();


            query = query.Where(filter);
            var total = await query.CountAsync();
            int pageCount = (int)Math.Ceiling(((double)total / itemsPerPage));
            var result = await query
                .OrderByDescending(orderByDescending)
                .Skip(skip)
                .Take(itemsPerPage)
                .ToListAsync();

            return new PagedList<TEntity>()
            {
                Page = page,
                ItemsPerPage = itemsPerPage,
                PageCount = pageCount,
                TotalItems = total,
                Items = result
            };
        }

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
            _dbSet.Attach(entity);
            _applicationDbContext.Entry(entity).State = EntityState.Modified;
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter) => await _dbSet.CountAsync(filter);
        public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> filter) => await CountAsync(filter) > 0;


    }
}
