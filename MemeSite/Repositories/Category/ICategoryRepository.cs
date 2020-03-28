using MemeSite.Model;
using MemeSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MemeSite.Repositories
{
    public interface ICategoryRepository
    {
        List<CategoryVM> GetCategories();
        Category PutCategory(CreateCategoryVM categoriesVM, int id);
        Category PostCategory(CreateCategoryVM categoriesVM);
        bool IsInDatabase(string name);
        bool DeleteCategory(int id);//delete only if there is no memes with the category
    }
}
