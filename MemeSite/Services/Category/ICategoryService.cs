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
        Task<List<Category>> GetAllCategories();
        Task<Category> GetById(int id);
        Task<Category> GetByName(string name);
        Task<bool> DeleteCategory(int id);
        Task<bool> DeleteCategory(Category category);
        Task InsertCategory(CreateCategoryVM category);
        Task<bool> UpdateCategory(CreateCategoryVM category, int id);
    }
}
