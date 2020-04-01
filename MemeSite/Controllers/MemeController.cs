using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemeSite.Repositories;
using MemeSite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MemeSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemeController : ControllerBase
    {
        private readonly IMemeRepository _memeRepository;
        public static IWebHostEnvironment _environment;

        public MemeController(IWebHostEnvironment webHostEnvironment, IMemeRepository memeRepository)
        {
            _memeRepository = memeRepository;
            _environment = webHostEnvironment;
        }

        [HttpGet("{page}/{items}")]
        public MemePagedListVM GetPagedListAcceptedContent(int page, int items)
        {
            var memes = _memeRepository.GetPagedMemes(page, items);
            return memes;
        }

        [HttpGet("unAccepted/{page}/{items}")]
        public MemePagedListVM GetPagedListUnacceptedContent(int page, int items)
        {
            var memes = _memeRepository.GetUnacceptedMemes(page, items);
            return memes;
        }

        [HttpGet("{categoryName}/{page}/{items}")]
        public MemePagedListVM GetPagedListByCategory(string categoryName, int page, int items)
        {
            var memes = _memeRepository.GetPagedMemesByCategory(categoryName, page, items);
            return memes;
        }

        [HttpGet("Archivized/{page}/{items}")]
        public MemePagedListVM GetArchivizedContent(int page, int items)
        {
            var memes = _memeRepository.GetArchivizedMemes(page, items);
            return memes;
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddMeme([FromBody] MemeUploadVM model)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            _memeRepository.UploadMeme(model, userId);
            return Ok(model);
        }

        [HttpGet("{memeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public MemeDetailsVM MemeDetails(int memeId)
        {
            var meme = _memeRepository.GetMemeDetailsById(memeId);
            return meme;
        }

        [HttpPut("{memeId}")]
        [Authorize]
        public IActionResult EditMeme(int id)
        {
            return Ok();
        }

        [HttpDelete("{memeId}")]
        [Authorize]
        public IActionResult DeleteMeme()
        {
            return Ok();
        }

        [HttpGet("GetRate/{id}")]
        public int AfterVote(int id)
        {
            int newRate = _memeRepository.GetMemeRate(id);
            return newRate;
        }




        
    }
}