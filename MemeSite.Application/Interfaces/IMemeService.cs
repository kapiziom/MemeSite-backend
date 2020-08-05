using MemeSite.Application.ViewModels;
using MemeSite.Domain.Models;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MemeSite.Application.Interfaces
{
    public interface IMemeService : IGenericService<Meme>
    {
        Task<Result<Meme>> Upload(MemeUploadVM model, string userId);
        Task<MemeDetailsVM> GetMemeDetailsById(int id, System.Security.Claims.ClaimsPrincipal user);
        //paged list with all includes for map
        Task<PagedList<MemeVM>> GetPagedMemesAsync<TKey>(Expression<Func<Meme, bool>> filter, Expression<Func<Meme, TKey>> order, int page, int itemsPerPage, System.Security.Claims.ClaimsPrincipal user);
        Task<PagedList<MemeVM>> GetPagedFavouritesMemesAsync(int page, int itemsPerPage, System.Security.Claims.ClaimsPrincipal user);
        Task<PagedList<MemeVM>> GetPagedUsersFavourites(int page, int itemsPerPage, System.Security.Claims.ClaimsPrincipal user);
        Task DeleteMeme(int id, System.Security.Claims.ClaimsPrincipal user);
        Task<Result<Meme>> EditMeme(EditMemeVM meme, int id, System.Security.Claims.ClaimsPrincipal user);
        Task ChangeArchiveStatus(int memeId, bool value);
        Task ChangeAccpetanceStatus(int memeId, bool value);

        Task<object> GetUsersFavourites(System.Security.Claims.ClaimsPrincipal user);

    }
}
