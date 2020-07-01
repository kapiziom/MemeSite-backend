using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MemeSite.Domain;
using MemeSite.Domain.Common;
using MemeSite.Api.Middleware;
using MemeSite.Api.Services;
using MemeSite.Api.ViewModels;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), 400)]
        [ProducesResponseType(typeof(ExceptionMessage), 409)]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryVM createCategoryVM)
        {
            var result = await _categoryService.InsertCategory(createCategoryVM);
            return Ok(result);
        }

        [HttpGet]
        public async Task<List<CategoryVM>> GetAllCategories()
            => await _categoryService.GetCategoriesVM();

        [HttpGet("{id}")]
        public async Task<CategoryVM> GetCategory(int id)
            => await _categoryService.GetCategoryVM(id);

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ExceptionMessage), 404)]
        [ProducesResponseType(typeof(ExceptionMessage), 409)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategory(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), 400)]
        [ProducesResponseType(typeof(ExceptionMessage), 404)]
        [ProducesResponseType(typeof(ExceptionMessage), 409)]
        public async Task<IActionResult> UpdateCategory([FromBody] CreateCategoryVM category, int id)
        {
            Result<Category> result = await _categoryService.UpdateCategory(category, id);
            return Ok(result.Value);
        }


    }

}