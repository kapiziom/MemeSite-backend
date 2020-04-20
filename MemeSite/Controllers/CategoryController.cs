using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemeSite.Model;
using MemeSite.Repository;
using MemeSite.Services;
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
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<object> CreateCategory([FromBody] CreateCategoryVM category)
            => await _categoryService.InsertCategory(category);

        [HttpGet]
        public async Task<List<CategoryVM>> GetAllCategories()
            => await _categoryService.GetCategoriesVM();

        [HttpGet("{id}")]
        public async Task<CategoryVM> GetCategory(int id)
            => await _categoryService.GetCategoryVM(id);

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<object> DeleteCategory(int id)
            => await _categoryService.DeleteCategory(id);

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<object> UpdateCategory([FromBody] CreateCategoryVM category, int id)
            => await _categoryService.UpdateCategory(category, id);


    }
}