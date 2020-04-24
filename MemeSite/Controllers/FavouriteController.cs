using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> InsertFavourite([FromBody] AddFavouriteVM fav)
        {
            bool result = await _service.InsertFavourite(fav);
            if (result == false)
            {
                return BadRequest();
            }
            return Ok(fav);
        }

        [HttpDelete("DeleteFromFavourite/{memeId}")]
        [Authorize]
        public async Task<IActionResult> DeleteFavourite(int memeId)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            bool result = await _service.DeleteFavourite(memeId, userId);
            if (result == false)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}