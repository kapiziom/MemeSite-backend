using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class VoteController : ControllerBase
    {
        private readonly IVoteService _voteService;

        public VoteController(IVoteService voteService)
        {
            _voteService = voteService;
        }


        [HttpPost("SendVote")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> InsertVote([FromBody] SendVoteVM vote)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            bool result = await _voteService.InsertVote(vote, userId);
            if (!ModelState.IsValid || result == false)
            {
                return BadRequest(new { error = "only 1 and -1 are accepted" });
            }
            return Ok(vote);
        }

        [HttpPut("SendVote")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Authorize]
        public async Task<IActionResult> UpdateVote([FromBody] SendVoteVM vote)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            if (!ModelState.IsValid)
            {
                return BadRequest(new { error = "only 1 and -1 are accepted" });
            }
            bool result = await _voteService.UpdateVote(vote, userId);
            if (result == true)
            {
                return Ok(vote);
            }
            else return Conflict(new { error = "U voted for this option" });
        }

        [HttpGet("GetMemeRate/{memeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<int> GetMemeRate(int memeId)
        {
            int newRate = await _voteService.GetMemeRate(memeId);
            return newRate;
        }


    }
}