using MemeSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using System.Web.Http;
using MemeSite.Data.Models;
using MemeSite.Data.Repository;
using MemeSite.Data.Models.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Net;
using MemeSite.Data.Models.Exceptions;

namespace MemeSite.Services
{
    public class CategoryService : GenericService<Category>, ICategoryService
    {

        public CategoryService(
            IGenericRepository<Category> categoryRepository,
            IValidator<Category> validator) : base(categoryRepository, validator) { }

        public async Task<CategoryVM> GetCategoryVM(int id)
        {
            var entity = await _repository.FindAsync(id);
            return GetCategoryVM(entity);
        }

        public CategoryVM GetCategoryVM(Category entity)
        {
            var category = new CategoryVM()
            {
                CategoryId = entity.CategoryId,
                CategoryName = entity.CategoryName,
            };
            return category;
        }

        public async Task<List<CategoryVM>> GetCategoriesVM()
        {
            var categories = await _repository.GetAllAsync();
            List<CategoryVM> list = new List<CategoryVM>();
            foreach(var m in categories)
            {
                CategoryVM item = new CategoryVM()
                {
                    CategoryId = m.CategoryId,
                    CategoryName = m.CategoryName,
                };
                list.Add(item);
            }
            return list;
        }

        public async Task DeleteCategory(int id)
        {
            if (await _repository.FindAsync(id) == null)
                throw new MemeSiteException(HttpStatusCode.NotFound, "Category not found");
            if (GetCountOfAssignedItems(id) > 0)
                throw new MemeSiteException(HttpStatusCode.Conflict, "Delete impossible. There are items assigned to this category");
            await _repository.DeleteAsync(id);
        }

        public async Task<Result<Category>> InsertCategory(CreateCategoryVM create)
        {
            Category entity = new Category()
            {
                CategoryName = create.CategoryName
            };
            var result = Validate(entity);
            if(await IsExistAsync(m => m.CategoryName == create.CategoryName))
            {
                throw new MemeSiteException(HttpStatusCode.Conflict, "Duplicate, category already exist.");
            }
            if (result.Succeeded)
            {
                result.Value = await _repository.InsertAsync(entity);
            }
            return result;
        }

        public async Task<Result<Category>> UpdateCategory(CreateCategoryVM categoryVM, int id)
        {
            var category = await FindAsync(id);
            if (category == null)
                throw new MemeSiteException(HttpStatusCode.NotFound, "Category not found");
            if (await IsExistAsync(m => m.CategoryName == categoryVM.CategoryName))
            {
                throw new MemeSiteException(HttpStatusCode.Conflict, "Duplicate, category already exist.");
            }
            category.CategoryName = categoryVM.CategoryName;
            return await Update(category);
        }

        public int? GetCountOfAssignedItems(int id)
        {
            var categories = _repository.Query().Include(x => x.Memes).Where(m => m.CategoryId == id);
            var category = categories.FirstOrDefault();
            return category?.Memes.Count() ?? 0;
        }

    }
}
