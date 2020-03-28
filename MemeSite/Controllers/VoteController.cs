using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemeSite.Repositories;
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
        public IActionResult AddVote()
        {
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        public IActionResult DeleteVote()
        {
            return Ok();
        }
    }
}