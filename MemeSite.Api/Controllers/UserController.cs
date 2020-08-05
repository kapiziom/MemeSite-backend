using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemeSite.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MemeSite.Domain.Models;
using MemeSite.Application.Interfaces;

namespace MemeSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("userStats/{userName}")]
        public async Task<UserStatsVM> GetUserStats(string userName) =>
            await _userService.GetUserStatsByName(userName);

        [HttpGet("ListUsersForAdmin/{page}/{itemsPerPage}")]
        [Authorize(Roles = "Administrator")]
        public async Task<PagedList<ListedUserVM>> GetUsers(int page, int itemsPerPage)
            => await _userService.GetPagedListVM(m => m.Id.Length > 0, m => m.CreationDate, page, itemsPerPage);



    }
}