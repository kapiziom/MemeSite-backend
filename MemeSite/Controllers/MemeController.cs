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
            var memes = _memeRepository.GetPagedMemes(page, items, User);
            return memes;
        }

        [HttpGet("unAccepted/{page}/{items}")]
        public MemePagedListVM GetPagedListUnacceptedContent(int page, int items)
        {
            var memes = _memeRepository.GetUnacceptedMemes(page, items, User);
            return memes;
        }

        [HttpGet("{categoryName}/{page}/{items}")]
        public MemePagedListVM GetPagedListByCategory(string categoryName, int page, int items)
        {
            var memes = _memeRepository.GetPagedMemesByCategory(categoryName, page, items, User);
            return memes;
        }

        [HttpGet("Archivized/{page}/{items}")]
        public MemePagedListVM GetArchivizedContent(int page, int items)
        {
            var memes = _memeRepository.GetArchivedMemes(page, items, User);
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
        public async Task<MemeDetailsVM> MemeDetails(int memeId)
        {
            var meme = await _memeRepository.GetMemeDetailsById(memeId, User);
            return meme;
        }

        [HttpGet("{memeId}/12")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public MemeDetailsVM MemeDetails2(int memeId)
        {
            //var meme = new MemeDetailsVM();
            var meme = _memeRepository.GetMemeDetailsById1(memeId, User);
            //if (User.Identity.IsAuthenticated == true)
            //{

            //    string userId = User.Claims.First(c => c.Type == "UserID").Value;
            //    meme = _memeRepository.GetMemeDetailsById1(memeId, userId, user);
            //}
            //else
            //{
            //meme = _memeRepository.GetMemeDetailsById1(memeId, System.Security.Claims.ClaimsPrincipal.Current);
            //}
            return meme;
        }

        [HttpPut("{memeId}")]
        [Authorize]
        public IActionResult EditMeme(int memeId)
        {
            return Ok();
        }



        [HttpPut("ChangeAccpetanceStatus/{memeId}/{value}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AcceptanceStatus(int memeId, bool value)
        {
            await _memeRepository.ChangeAccpetanceStatus(memeId, value);
            return Ok();
        }

        [HttpPut("ChangeArchiveStatus/{memeId}/{value}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> ArchiveStatus(int memeId, bool value)
        {
            await _memeRepository.ChangeArchiveStatus(memeId, value);
            return Ok();
        }

        [HttpDelete("{memeId}")]
        [Authorize]
        public IActionResult DeleteMeme()
        {
            return Ok();
        }

        
    }
}