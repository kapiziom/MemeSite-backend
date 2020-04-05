using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemeSite.Model;
using MemeSite.Repositories;
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
        private readonly ICommentRepository _commentRepository;
        private readonly ICommentService _commentService;
        public CommentController(ICommentRepository commentRepository, ICommentService commentService)
        {
            _commentRepository = commentRepository;
            _commentService = commentService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<CommentVM>> GetComment(int id)
        {
            CommentVM comment = await _commentRepository.GetCommentVM(id);
            return Ok(comment);
        }

        [HttpGet("List/{memeId}")]
        public List<CommentVM> GetComments(int memeId)
        {
            List<CommentVM> comments = _commentRepository.GetCommentsAssignedToMeme(memeId);
            return comments;
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddComment(AddCommentVM comment)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            _commentRepository.AddComment(comment, userId);
            return Ok(comment);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult EditComment(AddCommentVM comment, int id)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var result = _commentRepository.EditComment(comment, id, userId);
            if (result == true)
            {
                return Ok(comment);
            }
            else return BadRequest();
        }

        [HttpPut("DeleteTxt/{id}")]
        [Authorize]
        public IActionResult DeleteEditComment(int id)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            string role = User.Claims.First(c => c.Type == "role").Value;
            var result = _commentRepository.DeleteEditComment(id, userId, role);
            if (result == true)
            {
                return Ok();
            }
            else return BadRequest();
        }

        [HttpGet("countof/{memeId}")]
        public async Task<ActionResult<int>> countofmeme(int memeId)
        {
            return await _commentService.Count(x => x.MemeRefId == memeId);
        }

        [HttpGet("test/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Comment>> GetCommentById(int id)
        {
            Comment comment = await _commentService.FindAsync(id);
            return Ok(comment);
        }

    }


}