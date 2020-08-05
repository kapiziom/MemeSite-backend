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
            await _voteService.InsertVote(vote, userId);
            return Ok(new { message = "Voted successful", currentRate = await _voteService.GetMemeRate(vote.MemeRefId) });
        }

        [HttpPut("SendVote")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), 400)]
        [ProducesResponseType(typeof(ExceptionMessage), 409)]
        [Authorize]
        public async Task<IActionResult> UpdateVote([FromBody] SendVoteVM vote)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            await _voteService.UpdateVote(vote, userId);
            return Ok(new { message = "Voted successful", currentRate = await _voteService.GetMemeRate(vote.MemeRefId) });
        }

        [HttpGet("GetMemeRate/{memeId}")]
        public async Task<int> GetMemeRate(int memeId)
            => await _voteService.GetMemeRate(memeId);


    }
}