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

        public async Task<object> DeleteCategory(int id)
        {
            if (await FindAsync(id) == null)
            {
                return new NotFoundObjectResult(new { error = "Not Found" });
            }
            if(GetCountOfAssignedItems(id) > 0)
            {
                return new ConflictObjectResult(new { error = "Delete is impossible. There are items assigned to this category" });
            }
            await _repository.DeleteAsync(id);
            return new OkObjectResult(new { message = "Deleted" });
        }

        public async Task<object> InsertCategory(CreateCategoryVM categoryVM)
        {
            if (await IsExistAsync(m => m.CategoryName == categoryVM.CategoryName))
            {
                return new ConflictObjectResult(new { error = "Category already exist" });
            }
            Category category = new Category()
            {
                CategoryName = categoryVM.CategoryName,
            };
            await _repository.InsertAsync(category);
            return new StatusCodeResult(201);
        }


        public async Task<object> UpdateCategory(CreateCategoryVM categoryVM, int id)
        {
            if (await IsExistAsync(m => m.CategoryName == categoryVM.CategoryName))
            {
                return new ConflictObjectResult(new { error = "Category already exist" });
            }
            var category = await FindAsync(id);
            if (await FindAsync(id) == null)
            {
                return new NotFoundObjectResult(new { error = "Not Found" });
            }
            category.CategoryName = categoryVM.CategoryName;
            await _repository.UpdateAsync(category);
            return new OkObjectResult(categoryVM);
        }
            

    }
}
