using MemeSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MemeSite.Repositories
{
    public interface IMemeRepository
    {
        void UploadMeme(MemeUploadVM m, string userId);
        List<MemeVM> GetMemes();
        MemePagedListVM GetPagedMemes(int page);
        int GetMemeRate(int id);
        
    }
}
