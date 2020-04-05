using MemeSite.ViewModels;
using MemeSite.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MemeSite.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace MemeSite.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext _applicationDbContext) : base(_applicationDbContext)
        {
            if (_applicationDbContext.Categories.Count() == 0)
            {
                _applicationDbContext.Categories.AddRange(new List<Category>
                {
                    new Category{CategoryName = "Funny", Memes = null},
                    new Category{CategoryName = "Sport", Memes = null},
                    new Category{CategoryName = "Animals", Memes = null},
                    new Category{CategoryName = "Other", Memes = null}
                });
                _applicationDbContext.SaveChanges();
            }
        }


        public CategoryVM GetCategoryVM(int id)
        {
            var category = _applicationDbContext.Categories.FirstOrDefault(m => m.CategoryId == id);
            var categoryVM = new CategoryVM()
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
            };
            return categoryVM;
        }

        public async Task<Category> GetById(int id) => 
            await _applicationDbContext.Categories
            .FirstOrDefaultAsync(m => m.CategoryId == id);

        public async Task<Category> Get(Category category) => 
            await _applicationDbContext.Categories
            .FirstOrDefaultAsync(m => m.CategoryId == category.CategoryId && m.CategoryName == category.CategoryName);

        public async Task<Category> GetByName(string name) => 
            await _applicationDbContext.Categories
            .FirstOrDefaultAsync(m => m.CategoryName == name);

        public async Task<List<Category>> GetAllCategories() => await _applicationDbContext.Categories.ToListAsync();

        public async Task DeleteCategory1(int id)
        {
            var category = await GetById(id);
            _applicationDbContext.Categories.Remove(category);
            await _applicationDbContext.SaveChangesAsync();
        }
        public async Task DeleteCategory1(Category category)
        {
            _applicationDbContext.Categories.Remove(category);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task InsertCategory(Category category)
        {
            await _applicationDbContext.Categories.AddAsync(category);
            await _applicationDbContext.SaveChangesAsync();
        }
        public async Task UpdateCategory(Category category)
        {
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
