using MemeSite.ViewModels;
using MemeSite.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MemeSite.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MemeSite.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CategoryRepository(ApplicationDbContext context)
        {
            _applicationDbContext = context;
            if (_applicationDbContext.Categories.Count() == 0)
            {
                _applicationDbContext.Categories.AddRange(new List<Category>
                {
                    new Category{CategoryName = "Funny"},
                    new Category{CategoryName = "Sport"},
                    new Category{CategoryName = "Animals"},
                    new Category{CategoryName = "Other"}
                });
                _applicationDbContext.SaveChanges();
            }
        }

        public List<CategoryVM> GetCategories()
        {
            List<Category> items = _applicationDbContext.Categories.ToList();
            List<CategoryVM> categories = new List<CategoryVM>();
            foreach (var c in items)
            {
                CategoryVM category = new CategoryVM()
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName
                };
                categories.Add(category);
            }
            return categories.ToList();
        }


        public Category PostCategory(CreateCategoryVM categoriesVM)
        {
            Category category = new Category()
            {
                CategoryName = categoriesVM.CategoryName
            };
            _applicationDbContext.Categories.Add(category);
            _applicationDbContext.SaveChanges();
            return category;
        }

        public bool IsInDatabase(string name)
        {
            var categorymodel = _applicationDbContext.Categories.FirstOrDefault(m => m.CategoryName == name);
            if (categorymodel == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Category PutCategory(CreateCategoryVM categoriesVM, int id)
        {
            var category = _applicationDbContext.Categories.FirstOrDefault(m => m.CategoryId == id);
            category.CategoryName = categoriesVM.CategoryName;
            _applicationDbContext.SaveChanges();
            return category;
        }

        public bool DeleteCategory(int id)
        {
            var category = _applicationDbContext.Categories.FirstOrDefault(m => m.CategoryId == id);
            var meme = _applicationDbContext.Memes.FirstOrDefault(m => m.CategoryId == category.CategoryId);
            if (meme == null)
            {
                _applicationDbContext.Categories.Remove(category);
                _applicationDbContext.SaveChanges();
                return true;
            }
            else return false;
        }


    }
}
