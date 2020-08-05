using MemeSite.Application.ViewModels;
using MemeSite.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MemeSite.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserStatsVM> GetUserStatsByName(string userName);
        Task<UserStatsVM> GetUserStatsById(string userId);
        Task<UserStatsVM> GetUserStats(PageUser user);
        Task<PagedList<ListedUserVM>> GetPagedListVM<TKey>(Expression<Func<PageUser, bool>> filter, Expression<Func<PageUser, TKey>> order, int page, int itemsPerPage);
        Task<IdentityResult> RegisterUser(RegisterVM model);

        Task<IdentityResult> ChangePassword(ChangePasswordVM changePasswordVM, System.Security.Claims.ClaimsPrincipal user);
        Task<IdentityResult> ChangeEmail(ChangeEmailVM email, System.Security.Claims.ClaimsPrincipal user);
        Task<IdentityResult> SetUserRole(SetUserRoleVM setRole, System.Security.Claims.ClaimsPrincipal currentUser);

    }
}
