using MemeSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MemeSite.Repositories
{
    public interface IMemeRepository
    {
        void UploadMeme(MemeUploadVM m, string userId);
        MemeDetailsVM GetMemeDetailsById(int id);
        MemePagedListVM GetPagedMemes(int page, int itemsPerPage);//get accepted=true memes as paged list
        MemePagedListVM GetPagedMemesByCategory(string category, int page, int itemsPerPage);//get all memes assigned to category as paged list
        MemePagedListVM GetUnacceptedMemes(int page, int itemsPerPage);// get accepted=false memes paged list
        MemePagedListVM GetArchivizedMemes(int page, int itemsPerPage);
        int GetMemeRate(int id);//get rate of meme  {id}
        
    }
}
