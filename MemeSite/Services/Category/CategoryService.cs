using AutoMapper;
using MemeSite.Repository;
using MemeSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemeSite.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MemeSite.Services
{
    public class CategoryService : GenericService<Category>, ICategoryService
    {

        public CategoryService(IGenericRepository<Category> categoryRepository) : base(categoryRepository) 
        {
        }

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

        public int GetCountOfAssignedItems(int id)
        {
            var categories = _repository.Query().Include(x => x.Memes).Where(m => m.CategoryId == id);
            var category = categories.FirstOrDefault();
            return category.Memes.Count();
        }

        public async Task<bool> DeleteCategory(int id)
        {
            if(await FindAsync(id) == null || GetCountOfAssignedItems(id) > 0)
            {
                return false;
            }
            await _repository.DeleteAsync(id);
            return true;
        }

        public async Task InsertCategory(CreateCategoryVM categoryVM)
        {
            Category category = new Category()
            {
                CategoryName = categoryVM.CategoryName,
            };
            await _repository.InsertAsync(category);
        }

        public async Task<bool> UpdateCategory(CreateCategoryVM categoryVM, int id)
        {
            var category = await FindAsync(id);
            if (category != null)
            {
                category.CategoryName = categoryVM.CategoryName;
                await _repository.UpdateAsync(category);
                return true;
            }
            else return false;
        }
            

    }
}
