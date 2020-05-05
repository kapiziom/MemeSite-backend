using FluentValidation;
using MemeSite.Data.Models;
using MemeSite.Data.Models.Common;
using MemeSite.Data.Models.Exceptions;
using MemeSite.Data.Repository;
using MemeSite.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace MemeSite.Services
{
    public class UserService : IUserService
    {
        private readonly IMemeService _memeService;
        private readonly ICommentService _commentService;
        private readonly IFavouriteService _favouriteService;
        private readonly UserManager<PageUser> _userManager;
        private readonly IGenericRepository<PageUser> _userRepository;
        public UserService(
            IGenericRepository<PageUser> userRepository,
            IMemeService memeService,
            ICommentService commentService,
            IFavouriteService favouriteService,
            UserManager<PageUser> userManager)
        {
            _userRepository = userRepository;
            _memeService = memeService;
            _commentService = commentService;
            _favouriteService = favouriteService;
            _userManager = userManager;
        }

        public async Task<UserStatsVM> GetUserStatsById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }
            return await GetUserStats(user);
        }

        public async Task<UserStatsVM> GetUserStatsByName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
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
                UserRole = _userManager.GetRolesAsync(user).Result.FirstOrDefault(),
                TotalMemes = await _memeService.CountAsync(m => m.PageUser.UserName == user.UserName),
                TotalAccepted = await _memeService.CountAsync(m => m.PageUser.UserName == user.UserName && m.IsAccepted),
                TotalComments = await _commentService.CountAsync(m => m.PageUser.UserName == user.UserName),
                Joined = user.CreationDate.ToString("dd/MM/yyyy"),
            };
            return stats;
        }

        public async Task<PagedList<ListedUserVM>> GetPagedListVM<TKey>(
            Expression<Func<PageUser, bool>> filter,
            Expression<Func<PageUser, TKey>> order,
            int page, int itemsPerPage)
        {
            var model = await _userRepository.GetPagedAsync(filter, order, page, itemsPerPage);
            var VM = new PagedList<ListedUserVM>();
            VM.ItemsPerPage = model.ItemsPerPage;
            VM.Page = model.Page;
            VM.PageCount = model.PageCount;
            VM.TotalItems = model.TotalItems;
            List<ListedUserVM> list = new List<ListedUserVM>();

            foreach (var m in model.Items)
            {
                list.Add(await MapListedUserVM(m));
            }
            VM.Items = list;
            return VM;
        }

        public async Task<IdentityResult> RegisterUser(RegisterVM model)
        {
            var user = new PageUser { UserName = model.UserName, Email = model.Email, CreationDate = DateTime.Now };
            try
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                await _userManager.AddToRoleAsync(user, "NormalUser");
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IdentityResult> ChangePassword(ChangePasswordVM changePasswordVM, System.Security.Claims.ClaimsPrincipal user)
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

        public async Task<IdentityResult> ChangeEmail(ChangeEmailVM email, System.Security.Claims.ClaimsPrincipal user)
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

        public async Task<ListedUserVM> MapListedUserVM(PageUser user)
        {
            var role = await _userManager.GetRolesAsync(user);
            ListedUserVM vm = new ListedUserVM();
            vm.UserId = user.Id;
            vm.UserName = user.UserName;
            vm.Email = user.Email;
            vm.CreationDate = user.CreationDate.ToString("dd/MM/yyyy hh:mm");
            vm.MemeCount = await _memeService.CountAsync(m => m.UserID == user.Id);
            vm.CommentCount = await _commentService.CountAsync(m => m.UserID == user.Id);
            vm.UserRole = role.FirstOrDefault();
            return vm;
        }

        public async Task<IdentityResult> SetUserRole(SetUserRoleVM setRole, System.Security.Claims.ClaimsPrincipal currentUser)
        {
            if (setRole.UserId == currentUser.Claims.First(c => c.Type == "UserID").Value)
            {
                throw new MemeSiteException(HttpStatusCode.Conflict, "You can't change your role");
            }
            var user = await _userManager.FindByIdAsync(setRole.UserId);
            var userRole = _userManager.GetRolesAsync(user).Result.FirstOrDefault();
            try
            {
                var result = await SetRole(user, userRole, setRole.Role);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IdentityResult> SetRole(PageUser user, string removeFrom, string addTo)
        {
            await _userManager.RemoveFromRoleAsync(user, removeFrom);
            return await _userManager.AddToRoleAsync(user, addTo);
        }

    }
}
