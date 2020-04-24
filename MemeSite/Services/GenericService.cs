using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using MemeSite.Repository;

namespace MemeSite.Services
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class
    {
        protected readonly IGenericRepository<TEntity> _repository;

        public GenericService(IGenericRepository<TEntity> genericRepository)
        {
            _repository = genericRepository;
        }

        public async Task<TEntity> FindAsync(params object[] keyValues) => await _repository.FindAsync(keyValues);
        public async Task<List<TEntity>> GetAllAsync() => await _repository.GetAllAsync();
        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter)
            => await _repository.GetAllFilteredAsync(filter);
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter)
            => await _repository.CountAsync(filter);
        public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> filter)
            => await _repository.IsExistAsync(filter);

    }
}
