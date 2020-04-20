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
        Task<object> DeleteCategory(int id);
        Task<object> InsertCategory(CreateCategoryVM categoryVM);
        Task<object> UpdateCategory(CreateCategoryVM category, int id);
        int GetCountOfAssignedItems(int id);
    }
}
