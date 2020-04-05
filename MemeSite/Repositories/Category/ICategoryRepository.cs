using MemeSite.Model;
using MemeSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace MemeSite.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        CategoryVM GetCategoryVM(int id);


        Task<Category> GetById(int id);
        Task<Category> GetByName(string name);
        Task<Category> Get(Category category);
        Task<List<Category>> GetAllCategories();
        Task DeleteCategory1(int id);
        Task DeleteCategory1(Category category);
        Task InsertCategory(Category category);
        Task UpdateCategory(Category category);

    }
}
