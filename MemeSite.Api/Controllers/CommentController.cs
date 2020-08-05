using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemeSite.Api.Middleware;
using MemeSite.Application.Interfaces;
using MemeSite.Application.ViewModels;
using MemeSite.Domain.Models;
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
        [Authorize(Roles = "Administrator,NormalUser")]
        [ProducesResponseType(typeof(CommentVM), 200)]
        [ProducesResponseType(typeof(Result), 400)]
        public async Task<IActionResult> PostComment([FromBody] AddCommentVM comment)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var result = await _commentService.InsertComment(comment, userId);
            return Ok(result);//return new comment mapped to view model
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator,NormalUser")]
        [ProducesResponseType(typeof(CommentVM), 200)]
        [ProducesResponseType(typeof(Result), 400)]
        [ProducesResponseType(typeof(ExceptionMessage), 403)]
        public async Task<IActionResult> EditComment(EditCommentVM comment, int id)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var result = await _commentService.UpdateComment(comment, id, userId);
            return Ok(result);//return modified comment mapped to view model
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,NormalUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteComment(int id)
        {
            await _commentService.DeleteComment(id, User);
            return NoContent();
        }

        [HttpPut("DelTxtAsAdmin/{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<CommentVM> DeleteCommentTxtAsAdmin(int id) => await _commentService.ArchiveTxtAsAdmin(id);


    }
}