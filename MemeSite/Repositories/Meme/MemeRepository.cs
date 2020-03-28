using MemeSite.ViewModels;
using MemeSite.Model;
using System;
using System.Collections.Generic;
using System.Text;
using MemeSite.Data;

namespace MemeSite.Repositories
{
    public class MemeRepository : IMemeRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public MemeRepository(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }

        public string UploadImage(FileUpload file)
        {
            return "elo";
        }

        public void UploadMeme(MemeUploadVM m, string userId)
        {
            var meme = new Meme()
            {
                Title = m.Title,
                Txt = m.Txt,
                CategoryId = m.CategoryId,
                //iformfile to do
                IsAccepted = false,
                IsArchivized = false,
                CreationDate = DateTime.Now,
                AccpetanceDate = null,
                UserID = userId,

            };
            _applicationDbContext.Memes.Add(meme);
            _applicationDbContext.SaveChanges();
        }
    }
}
