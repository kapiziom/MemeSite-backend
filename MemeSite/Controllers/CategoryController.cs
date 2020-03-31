using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemeSite.Repositories;
using MemeSite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MemeSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public List<CategoryVM> GetCategories()
        {
            var categories = _categoryRepository.GetCategories();
            return categories;
        }


        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult PostCategory([FromBody] CreateCategoryVM category)
        {
            var isInDb = _categoryRepository.IsInDatabase(category.CategoryName);
            if(isInDb == true)
            {
                return Conflict(new { message = "Category is already exist" });
            }
            else
            {
                var result = _categoryRepository.PostCategory(category);
                return Ok(result);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult PutCategory([FromBody] CreateCategoryVM category, int id)
        {
            var result = _categoryRepository.PutCategory(category, id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteCategory(int id)
        {
            var result = _categoryRepository.DeleteCategory(id);
            if (result == true)
            {
                return Ok();
            }
            else return BadRequest();
        }
    }
}