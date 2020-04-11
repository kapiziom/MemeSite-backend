using MemeSite.Model;
using MemeSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MemeSite.Services
{
    public interface IMemeService : IGenericService<Meme>
    {
        Task Upload(MemeUploadVM model, string userId);
        Task<MemeDetailsVM> GetMemeDetailsById(int id, System.Security.Claims.ClaimsPrincipal user);
        Task<PagedList<MemeVM>> GetPagedMemesAsync<TKey>(Expression<Func<Meme, bool>> filter, Expression<Func<Meme, TKey>> order, int page, int itemsPerPage, System.Security.Claims.ClaimsPrincipal user);
        Task<bool> DeleteMeme(int id, System.Security.Claims.ClaimsPrincipal user);
        Task<bool> EditMeme(EditMemeVM meme, int id, System.Security.Claims.ClaimsPrincipal user);
        Task ChangeArchiveStatus(int memeId, bool value);
        Task ChangeAccpetanceStatus(int memeId, bool value);
        Task<int> Count(Expression<Func<Meme, bool>> filter);

    }
}
