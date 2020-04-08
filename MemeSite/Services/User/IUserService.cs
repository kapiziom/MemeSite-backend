using MemeSite.Model;
using MemeSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.Services
{
    public interface IUserService : IGenericService<PageUser>
    {
        Task<UserStatsVM> GetUserStatsByName(string userName);
        Task<UserStatsVM> GetUserStatsById(string userId);
        Task<UserStatsVM> GetUserStats(PageUser user);
    }
}
