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
    public class MemeController : ControllerBase
    {
        private readonly IMemeRepository _memeRepository;

        public MemeController(IMemeRepository memeRepository)
        {
            memeRepository = _memeRepository;
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddMeme([FromBody] MemeUploadVM model)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            _memeRepository.UploadMeme(model, userId);
            return Ok(model);
        }

        
        [HttpPut]
        [Authorize]
        public IActionResult EditMeme()
        {
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        public IActionResult DeleteMeme()
        {
            return Ok();
        }
    }
}