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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<CommentVM>> GetComment(int id)
        {
            CommentVM comment = await _commentService.GetCommentVM(id);
            return Ok(comment);
        }

        [HttpGet("ListForMeme/{memeId}")]
        public async Task<List<CommentVM>> GetCommentsAssignedToMeme(int memeId)
        {
            List<CommentVM> comments = await _commentService.GetListCommentVM(m => m.MemeRefId == memeId);
            return comments;
        }

        [HttpGet("ListForUser/{userName}")]
        public async Task<List<CommentVM>> GetCommentsAssignedToUser(string userName)
        {
            List<CommentVM> comments = await _commentService.GetListCommentVM(m => m.PageUser.UserName == userName);
            return comments;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostComment([FromBody] AddCommentVM comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
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
            if (result != null)
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

    }
}