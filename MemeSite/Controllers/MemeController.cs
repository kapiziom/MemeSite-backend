using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemeSite.Model;
using MemeSite.Repository;
using MemeSite.Services;
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
        private readonly IMemeService _memeService;

        public MemeController(IMemeService memeService)
        {
            _memeService = memeService;
        }

        [HttpGet("{page}/{itemsPerPage}")]
        public async Task<PagedList<MemeVM>> GetPagedListAccepted(int page, int itemsPerPage) =>
            await _memeService.GetPagedMemesAsync(m => m.IsAccepted == true && m.IsArchived == false, 
                m => m.AccpetanceDate, page, itemsPerPage, User);


        [HttpGet("unAccepted/{page}/{itemsPerPage}")]
        public async Task<PagedList<MemeVM>> GetPagedListUnAccepted(int page, int itemsPerPage) =>
            await _memeService.GetPagedMemesAsync(m => m.IsAccepted == false && m.IsArchived == false, 
                m => m.CreationDate, page, itemsPerPage, User);

        [HttpGet("{categoryName}/{page}/{items}")]
        public async Task<PagedList<MemeVM>> GetPagedListByCategory(string categoryName, int page, int items) => 
            await _memeService.GetPagedMemesAsync(m => m.Category.CategoryName == categoryName && m.IsArchived == false,
                m => m.CreationDate, page, items, User);

        [HttpGet("Archive/{page}/{items}")]
        public async Task<PagedList<MemeVM>> GetPagedListArchive(int page, int items) =>
            await _memeService.GetPagedMemesAsync(m => m.IsArchived == true,
                m => m.CreationDate, page, items, User);

        [HttpGet("UserContent/{userName}/{page}/{itemsPerPage}")]
        public async Task<PagedList<MemeVM>> GetPagedListAssignedToUser(string userName, int page, int itemsPerPage) =>
            await _memeService.GetPagedMemesAsync(m => m.PageUser.UserName == userName,
                m => m.CreationDate, page, itemsPerPage, User);

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UploadMeme([FromBody] MemeUploadVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            await _memeService.Upload(model, userId);
            return Ok(model);
        }

        [HttpGet("{memeId}")]
        public async Task<MemeDetailsVM> MemeDetails(int memeId)
            => await _memeService.GetMemeDetailsById(memeId, User);

        [HttpPut("{memeId}")]
        [Authorize]
        public async Task<IActionResult> EditMeme(int memeId, [FromBody] EditMemeVM editMeme)
        {
            var result = await _memeService.EditMeme(editMeme, memeId, User);
            if (result == true)
            {
                return Ok();
            }
            else return BadRequest();
        }
            



        [HttpPut("ChangeAccpetanceStatus/{memeId}/{value}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AcceptanceStatus(int memeId, bool value)
        {
            await _memeService.ChangeAccpetanceStatus(memeId, value);
            return Ok();
        }

        [HttpPut("ChangeArchiveStatus/{memeId}/{value}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> ArchiveStatus(int memeId, bool value)
        {
            await _memeService.ChangeArchiveStatus(memeId, value);
            return Ok();
        }

        [HttpDelete("{memeId}")]
        [Authorize]
        public async Task<IActionResult> DeleteMeme(int memeId)
        {
            bool result = await _memeService.DeleteMeme(memeId, User);
            if(result == true)
            {
                return Ok();
            }
            return BadRequest();
        }

        
    }
}