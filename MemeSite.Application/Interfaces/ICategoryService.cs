using MemeSite.Application.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemeSite.Domain.Models;

namespace MemeSite.Application.Interfaces
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
