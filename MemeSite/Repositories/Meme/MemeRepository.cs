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

        public MemeDetailsVM GetMemeDetailsById(int id)
        {
            var meme = _applicationDbContext.Memes.FirstOrDefault(m => m.MemeId == id);
            if(meme == null)
            {
                return null;
            }
            var memeVM = new MemeDetailsVM()
            {
                MemeId = meme.MemeId,
                Title = meme.Title,
                UserName = _userRepository.GetUsernameById(meme.UserID),
                ByteHead = meme.ByteHead,
                ByteImg = meme.ImageByte,
                Category = _categoryRepository.GetCategoryVM(meme.CategoryId),
                CreationDate = meme.CreationDate.ToString("dd/MM/yyyy"),
                Rate = GetMemeRate(meme.MemeId),
                CommentCount = 100,//do zrobienia
            };
            return memeVM;
        }

        public MemePagedListVM GetPagedMemes(int page, int itemsPerPage)
        {
            var MemeList = new MemePagedListVM { PageCount = 1 };
            var MemesVM = new List<MemeVM>();
            List<Meme> memes = _applicationDbContext.Memes.Where(m => m.IsAccepted == true && m.IsArchivized == false).OrderByDescending(m => m.AccpetanceDate).ToList();

            //available pages
            MemeList.PageCount = (int)Math.Ceiling(((double)memes.Count() / itemsPerPage));

            memes = memes.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList();
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

        public MemePagedListVM GetPagedMemesByCategory(string category, int page, int itemsPerPage)
        {
            var MemeList = new MemePagedListVM { PageCount = 1 };
            var MemesVM = new List<MemeVM>();
            var categoryModel = _applicationDbContext.Categories.FirstOrDefault(m => m.CategoryName == category);
            List<Meme> memes = _applicationDbContext.Memes.Where(m => m.CategoryId == categoryModel.CategoryId && m.IsArchivized == false).OrderByDescending(m => m.CreationDate).ToList();
           
            //available pages
            MemeList.PageCount = (int)Math.Ceiling(((double)memes.Count() / itemsPerPage));

            memes = memes.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList();
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

        public MemePagedListVM GetUnacceptedMemes(int page, int itemsPerPage)
        {
            var MemeList = new MemePagedListVM { PageCount = 1 };
            var MemesVM = new List<MemeVM>();
            List<Meme> memes = _applicationDbContext.Memes.Where(m => m.IsAccepted == false && m.IsArchivized == false).OrderByDescending(m => m.CreationDate).ToList(); ;

            //available pages
            MemeList.PageCount = (int)Math.Ceiling(((double)memes.Count() / itemsPerPage));

            memes = memes.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList();
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

        public MemePagedListVM GetArchivizedMemes(int page, int itemsPerPage)
        {
            var MemeList = new MemePagedListVM { PageCount = 1 };
            var MemesVM = new List<MemeVM>();
            List<Meme> memes = _applicationDbContext.Memes.Where(m => m.IsArchivized == true).OrderByDescending(m => m.CreationDate).ToList(); ;

            //available pages
            MemeList.PageCount = (int)Math.Ceiling(((double)memes.Count() / itemsPerPage));

            memes = memes.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList();
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
