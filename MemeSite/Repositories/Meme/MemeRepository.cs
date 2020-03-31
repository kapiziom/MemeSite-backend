using MemeSite.ViewModels;
using MemeSite.Model;
using System;
using System.Collections.Generic;
using System.Text;
using MemeSite.Data;
using System.Linq;

namespace MemeSite.Repositories
{
    public class MemeRepository : IMemeRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;

        public MemeRepository(ApplicationDbContext context, IUserRepository userRepository, ICategoryRepository categoryRepository)
        {
            _applicationDbContext = context;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
        }

        public List<MemeVM> GetMemes()
        {
            List<Meme> items = _applicationDbContext.Memes.ToList();
            List<MemeVM> memes = new List<MemeVM>();
            
            foreach (var b in items)
            {
                MemeVM meme = new MemeVM()
                {
                    MemeId = b.MemeId,
                    Title = b.Title,
                    UserName = _userRepository.GetUsernameById(b.UserID),
                    ByteHead = b.ByteHead,
                    ByteImg = b.ImageByte,
                    Category = _categoryRepository.GetCategoryVM(b.CategoryId),
                    CreationDate = b.CreationDate.ToString("dd/MM/yyyy"),
                    Rate = 100,//do zrobienia
                    CommentCount = 100,//do zrobienia
                };
                memes.Add(meme);
            }
            return memes.ToList();
        }


        public void UploadMeme(MemeUploadVM model, string userId)
        {
            var category = _categoryRepository.GetCategoryById(model.CategoryId);
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

        public MemePagedListVM GetPagedMemes(int page)
        {
            int defaultPageCount = 1;
            var MemeList = new MemePagedListVM { PageCount = 1 };
            var MemesVM = new List<MemeVM>();
            List<Meme> memes = _applicationDbContext.Memes.OrderByDescending(m => m.CreationDate).ToList();

            //available pages
            MemeList.PageCount = (int)Math.Ceiling(((double)memes.Count() / defaultPageCount));

            memes = memes.Skip((page - 1) * defaultPageCount).Take(defaultPageCount).ToList();
            //create model
            foreach (var b in memes)
            {
                var meme = new MemeVM
                {
                    MemeId = b.MemeId,
                    Title = b.Title,
                    UserName = _userRepository.GetUsernameById(b.UserID),
                    ByteHead = b.ByteHead,
                    ByteImg = b.ImageByte,
                    Category = _categoryRepository.GetCategoryVM(b.CategoryId),
                    CreationDate = b.CreationDate.ToString("dd/MM/yyyy"),
                    Rate = GetMemeRate(b.MemeId),
                    CommentCount = 100,//do zrobienia
                };


                MemesVM.Add(meme);
            }
            MemeList.MemeList = MemesVM;
            return MemeList;
        }

        public int GetMemeRate(int id)
        {
            var plus = _applicationDbContext.Votes.Where(m => m.Value == 1 && m.MemeRefId == id).Count();
            var minus = _applicationDbContext.Votes.Where(m => m.Value == -1 && m.MemeRefId == id).Count();
            return plus-minus;
        }
    }
}
