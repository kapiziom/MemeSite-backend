using System;
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
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("{id}")]
        public async Task<CommentVM> GetComment(int id)
            => await _commentService.GetCommentVM(id);

        [HttpGet("ListForMeme/{memeId}")]
        public async Task<List<CommentVM>> GetCommentsAssignedToMeme(int memeId)
            => await _commentService.GetListCommentVM(m => m.MemeRefId == memeId);
        
        [HttpGet("PagedListForUser/{userName}/{page}/{itemsPerPage}")]
        public async Task<PagedList<CommentVM>> GetCommentsAssignedToUser(string userName, int page, int itemsPerPage)
            => await _commentService.GetPagedListVM(m => m.PageUser.UserName == userName, m => m.CreationDate, page, itemsPerPage);

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostComment([FromBody] AddCommentVM comment)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
                return BadRequest(errors);
            }
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var result = await _commentService.InsertComment(comment, userId);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> EditComment(EditCommentVM comment, int id)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var result = await _commentService.UpdateComment(comment, id, userId);
            if (result != null && ModelState.IsValid)
            {
                return Ok(result);
            }
            else return BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var result = await _commentService.DeleteComment(id, User);
            if (result == true)
            {
                return Ok();
            }
            else return BadRequest();
        }

        [HttpPut("DelTxtAsAdmin/{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<CommentVM> DeleteCommentTxtAsAdmin(int id) => await _commentService.DeleteTxtAsAdmin(id);


    }
}