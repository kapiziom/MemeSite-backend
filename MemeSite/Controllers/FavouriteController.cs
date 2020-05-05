using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MemeSite.Data.Models;
using MemeSite.Services;
using MemeSite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MemeSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteController : ControllerBase
    {
        private readonly IFavouriteService _service;
        public FavouriteController(IFavouriteService favouriteService)
        {
            _service = favouriteService;
        }

        [HttpPost("AddFavourite")]
        [Authorize]
        public async Task<IActionResult> InsertFavourite([FromBody] AddFavouriteVM AddFavourite)
        {
            AddFavourite.UserId = User.Claims.First(c => c.Type == "UserID").Value;
            await _service.InsertFavourite(AddFavourite);
            return Ok(new { successMessage = "Success"});
        }

        [HttpDelete("DeleteFromFavourite/{memeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize]
        public async Task<IActionResult> DeleteFavourite(int memeId)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            await _service.DeleteFavourite(memeId, userId);
            return NoContent();
        }
    }
}