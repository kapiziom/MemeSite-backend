using AutoMapper;
using MemeSite.Repositories;
using MemeSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemeSite.Model;
using Microsoft.AspNetCore.Mvc;

namespace MemeSite.Services
{
    public class CategoryService : GenericService<Category>, ICategoryService
    {
        public CategoryService(ICategoryRepository categoryRepository) : base(categoryRepository) { }

        public async Task<Category> GetByName(string name) => await _repository.FindAsync(m => m.CategoryName == name);

        public async Task<bool> DeleteCategory(int id)
        {
            if(await FindAsync(id) == null)
            {
                return false;
            }
            await _repository.DeleteAsync(id);
            return true;
        }
        public async Task<bool> DeleteCategory(Category category)
        {
            if (await FindAsync(category.CategoryId) == null)
            {
                return false;
            }
            await _repository.DeleteAsync(category);
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
