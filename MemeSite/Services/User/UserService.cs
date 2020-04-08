using MemeSite.Model;
using MemeSite.Repository;
using MemeSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.Services
{
    public class UserService : GenericService<PageUser>, IUserService
    {
        private readonly IMemeService _memeService;
        private readonly ICommentService _commentService;
        public UserService(IGenericRepository<PageUser> _userRepository,
            IMemeService memeService,
            ICommentService commentService) : base(_userRepository)
        {
            _memeService = memeService;
            _commentService = commentService;
        }

        public async Task<UserStatsVM> GetUserStatsById(string userId)
        {
            var user = await _repository.FindAsync(userId);
            if (user == null)
            {
                return null;
            }
            return await GetUserStats(user);
        }

        public async Task<UserStatsVM> GetUserStatsByName(string userName)
        {
            var user = await _repository.FindAsync(m => m.UserName == userName);
            if(user == null)
            {
                return null;
            }
            return await GetUserStats(user);
        }

        public async Task<UserStatsVM> GetUserStats(PageUser user)
        {
            UserStatsVM stats = new UserStatsVM()
            {
                UserId = user.Id,
                UserName = user.UserName,
                TotalMemes = await _memeService.CountAsync(m => m.PageUser.UserName == user.UserName),
                TotalAccepted = await _memeService.CountAsync(m => m.PageUser.UserName == user.UserName && m.IsAccepted),
                TotalComments = await _commentService.CountAsync(m => m.PageUser.UserName == user.UserName),
                Joined = user.CreationDate.ToString("dd/MM/yyyy"),
            };
            return stats;
        }
    }
}
