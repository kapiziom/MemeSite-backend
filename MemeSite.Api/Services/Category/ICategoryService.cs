using MemeSite.Domain;
using MemeSite.Domain.Common;
using MemeSite.Api.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.Api.Services
{
    public interface ICategoryService : IGenericService<Category>
    {
        Task<CategoryVM> GetCategoryVM(int id);
        CategoryVM GetCategoryVM(Category entity);
        Task<List<CategoryVM>> GetCategoriesVM();
        Task DeleteCategory(int id);
        Task<Result<Category>> InsertCategory(CreateCategoryVM category);
        Task<Result<Category>> UpdateCategory(CreateCategoryVM category, int id);
    }
}
