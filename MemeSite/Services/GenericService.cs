using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Core;
using Microsoft.AspNetCore.Mvc;
using MemeSite.Data.Repository;
using MemeSite.Data.Models.Common;

namespace MemeSite.Services
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class
    {
        protected readonly IGenericRepository<TEntity> _repository;
        protected readonly IValidator<TEntity> _validator;

        public GenericService(IGenericRepository<TEntity> genericRepository, IValidator<TEntity> validator)
        {
            _repository = genericRepository;
            _validator = validator;
        }

        public async Task<TEntity> FindAsync(params object[] keyValues) => await _repository.FindAsync(keyValues);
        public async Task<List<TEntity>> GetAllAsync() => await _repository.GetAllAsync();
        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter)
            => await _repository.GetAllFilteredAsync(filter);
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter)
            => await _repository.CountAsync(filter);
        public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> filter)
            => await _repository.IsExistAsync(filter);


        protected Result<TEntity> Validate(TEntity entity) => new Result<TEntity>(_validator.Validate(entity));
        
        protected async Task<Result<TEntity>> ValidateAsync(TEntity entity) => new Result<TEntity>(await _validator.ValidateAsync(entity));

        public async Task<Result<TEntity>> Insert(TEntity entity)
        {
            var result = await ValidateAsync(entity);

            if (result.Succeeded)
                result.Value = await _repository.InsertAsync(entity);

            return result;
        }

        public async Task<Result<TEntity>> Update(TEntity entity)
        {
            var result = await ValidateAsync(entity);

            if (result.Succeeded)
                result.Value = await _repository.UpdateAsync(entity);

            return result;
        }

        public async Task<List<TEntity>> GetAllFilteredIncludeAsync(Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, object>>[] includeExpressions)
            => await _repository.GetAllFilteredIncludeAsync(filter, includeExpressions);

    }
}
