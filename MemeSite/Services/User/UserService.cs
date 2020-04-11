using MemeSite.Model;
using MemeSite.Repository;
using MemeSite.ViewModels;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<PageUser> _userManager;
        public UserService(IGenericRepository<PageUser> _userRepository,
            IMemeService memeService,
            ICommentService commentService,
            UserManager<PageUser> userManager) : base(_userRepository)
        {
            _memeService = memeService;
            _commentService = commentService;
            _userManager = userManager;
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

        public async Task<object> ChangePassword(ChangePasswordVM changePasswordVM, System.Security.Claims.ClaimsPrincipal user)
        {
            string userId = user.Claims.First(c => c.Type == "UserID").Value;
            var usermodel = await _userManager.FindByIdAsync(userId);
            try
            {
                var result = await _userManager.ChangePasswordAsync(usermodel, changePasswordVM.OldPassword, changePasswordVM.NewPassword);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> ChangeEmail(ChangeEmailVM email, System.Security.Claims.ClaimsPrincipal user)
        {
            string userId = user.Claims.First(c => c.Type == "UserID").Value;

            var usermodel = await _userManager.FindByIdAsync(userId);
            try
            {
                usermodel.Email = email.NewEmail;
                usermodel.NormalizedEmail = email.NewEmail.Normalize();
                var result = await _userManager.UpdateAsync(usermodel);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<object> ChangeUserName(ChangeUserNameVM username, System.Security.Claims.ClaimsPrincipal user)
        {
            string userId = user.Claims.First(c => c.Type == "UserID").Value;
            var usermodel = await _userManager.FindByIdAsync(userId);
            try
            {
                var result = await _userManager.SetUserNameAsync(usermodel, username.NewUserName);
                await _userManager.UpdateNormalizedUserNameAsync(usermodel);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
