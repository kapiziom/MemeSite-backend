using MemeSite.Model;
using MemeSite.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.Services
{
    public interface ICategoryService : IGenericService<Category>
    {
        Task<CategoryVM> GetCategoryVM(int id);
        CategoryVM GetCategoryVM(Category entity);
        Task<List<CategoryVM>> GetCategoriesVM();
        Task<bool> DeleteCategory(int id);
        Task InsertCategory(CreateCategoryVM category);
        Task<bool> UpdateCategory(CreateCategoryVM category, int id);
        int GetCountOfAssignedItems(int id);
    }
}
