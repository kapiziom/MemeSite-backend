using MemeSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MemeSite.Repositories
{
    public interface IMemeRepository
    {
        void UploadMeme(MemeUploadVM m, string userId);
        MemeDetailsVM GetMemeDetailsById1(int id, System.Security.Claims.ClaimsPrincipal user);
        Task<MemeDetailsVM> GetMemeDetailsById(int id, System.Security.Claims.ClaimsPrincipal user);
        Task<MemePagedListVM> GetPagedMemesAsync(int page, int itemsPerPage, string category, bool IsAccepted, bool IsArchived);
        MemePagedListVM GetPagedMemes(int page, int itemsPerPage, System.Security.Claims.ClaimsPrincipal user);//get accepted=true memes as paged list
        MemePagedListVM GetPagedMemesByCategory(string category, int page, int itemsPerPage, System.Security.Claims.ClaimsPrincipal user);//get all memes assigned to category as paged list
        MemePagedListVM GetUnacceptedMemes(int page, int itemsPerPage, System.Security.Claims.ClaimsPrincipal user);// get accepted=false memes paged list
        MemePagedListVM GetArchivedMemes(int page, int itemsPerPage, System.Security.Claims.ClaimsPrincipal user);
        void ChangeArchiveStatus1(int memeId, bool value);
        void ChangeAccpetanceStatus1(int memeId, bool value);
        Task ChangeArchiveStatus(int memeId, bool value);
        Task ChangeAccpetanceStatus(int memeId, bool value);

    }
}
