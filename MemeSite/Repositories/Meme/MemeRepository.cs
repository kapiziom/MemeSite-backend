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

        public void UploadMeme(MemeUploadVM model, string userId)
        {
            var meme = new Meme()
            {
                Title = model.Title,
                UserID = userId,
                Txt = model.Txt,
                CategoryId = model.CategoryId,
                ImageName = model.FileName,
                ByteHead = model.ByteHead,
                ImageByte = model.FileByte,
                CreationDate = DateTime.Now,
                IsAccepted = false,
                IsArchivized = false,
                AccpetanceDate = null,
            };
            
            _applicationDbContext.Memes.Add(meme);
            _applicationDbContext.SaveChanges();
        }
    }
}
