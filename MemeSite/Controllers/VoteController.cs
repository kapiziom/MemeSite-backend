using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemeSite.Repositories;
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
        private readonly IVoteRepository _voteRepository;

        public VoteController(IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }

        [HttpPost]
        [Authorize]
        public IActionResult SendVote([FromBody] SendVoteVM vote)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            bool result = _voteRepository.SendVote(vote, userId);
            if (result == true)
            {
                return Ok(vote);
            }
            else return Conflict();
            
        }

        

    }
}