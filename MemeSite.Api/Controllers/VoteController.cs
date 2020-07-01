using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemeSite.Domain.Common;
using MemeSite.Api.Middleware;
using MemeSite.Api.Services;
using MemeSite.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MemeSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly IVoteService _voteService;

        public VoteController(IVoteService voteService)
        {
            _voteService = voteService;
        }


        [HttpPost("SendVote")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), 400)]
        [ProducesResponseType(typeof(ExceptionMessage), 409)]
        [Authorize]
        public async Task<IActionResult> InsertVote([FromBody] SendVoteVM vote)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var result = await _voteService.InsertVote(vote, userId);
            return Ok(result.Value.Value);
        }

        [HttpPut("SendVote")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), 400)]
        [ProducesResponseType(typeof(ExceptionMessage), 409)]
        [Authorize]
        public async Task<IActionResult> UpdateVote([FromBody] SendVoteVM vote)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var result = await _voteService.UpdateVote(vote, userId);
            return Ok(result.Value.Value);
        }

        [HttpGet("GetMemeRate/{memeId}")]
        public async Task<int> GetMemeRate(int memeId)
            => await _voteService.GetMemeRate(memeId);


    }
}