using MemeSite.ViewModels;
using MemeSite.Model;
using System;
using System.Collections.Generic;
using System.Text;
using MemeSite.Data;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MemeSite.Repositories
{
    public class MemeRepository : IMemeRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IVoteRepository _voteRepository;

        public MemeRepository(ApplicationDbContext context,
            IUserRepository userRepository,
            ICategoryRepository categoryRepository,
            IVoteRepository voteRepository)
        {
            _applicationDbContext = context;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _voteRepository = voteRepository;
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
                IsArchived = false,
                AccpetanceDate = null,
            };
            
            _applicationDbContext.Memes.Add(meme);
            _applicationDbContext.SaveChanges();
        }

        public async Task<MemeDetailsVM> GetMemeDetailsById(int id, System.Security.Claims.ClaimsPrincipal user)
        {
            var meme = await _applicationDbContext.Memes.FirstOrDefaultAsync(m => m.MemeId == id);
            if (meme == null)
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
                Rate = _voteRepository.GetMemeRate(meme.MemeId),
                CommentCount = _applicationDbContext.Comments.Where(m => m.MemeRefId == meme.MemeId).Count(),
                IsAccepted = meme.IsAccepted,
                IsArchived = meme.IsArchived,
                IsVoted = false,
                IsFavourite = false,
                VoteValue = null,
            };
            if (user != null && user.Identity.IsAuthenticated == true)
            {
                string userId = user.Claims.First(c => c.Type == "UserID").Value;
                memeVM.IsVoted = _voteRepository.DidUserVote(id, userId);
                memeVM.VoteValue = _applicationDbContext.Votes.FirstOrDefaultAsync(m => m.MemeRefId == meme.MemeId && m.UserId == userId).Result.Value;
                memeVM.IsFavourite = false;
            }
            return memeVM;
        }

        public async Task<MemePagedListVM> GetPagedMemesAsync(int page, int itemsPerPage, string category, bool IsAccepted, bool IsArchived)
        {
            var MemeList = new MemePagedListVM { PageCount = 1 };
            var MemesVM = new List<MemeVM>();
            List<Meme> memes = _applicationDbContext.Memes.Where(m => m.IsAccepted == IsAccepted && m.IsArchived == IsArchived).ToList();

            

            memes = memes.OrderByDescending(m => m.AccpetanceDate).ToList();
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
                    Rate = _voteRepository.GetMemeRate(b.MemeId),
                    CommentCount = _applicationDbContext.Comments.Where(m => m.MemeRefId == b.MemeId).Count(),
                    IsAccepted = b.IsAccepted,
                    IsArchived = b.IsArchived,
                    IsVoted = false,
                    VoteValue = null,
                    IsFavourite = false,
                };
                MemesVM.Add(meme);
            }
            MemeList.MemeList = MemesVM;
            return MemeList;
        }

        public MemeDetailsVM GetMemeDetailsById1(int id, System.Security.Claims.ClaimsPrincipal user)
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
                Rate = _voteRepository.GetMemeRate(meme.MemeId),
                CommentCount = _applicationDbContext.Comments.Where(m => m.MemeRefId == meme.MemeId).Count(),
                IsAccepted = meme.IsAccepted,
                IsArchived = meme.IsArchived,
                IsVoted = false,
                IsFavourite = false,
                VoteValue = null,
            };
            if (user != null && user.Identity.IsAuthenticated == true)
            {
                string userId = user.Claims.First(c => c.Type == "UserID").Value;
                memeVM.IsVoted = _voteRepository.DidUserVote(id, userId);
                memeVM.VoteValue = _applicationDbContext.Votes.FirstOrDefaultAsync(m => m.MemeRefId == meme.MemeId && m.UserId == userId).Result.Value;
                memeVM.IsFavourite = false;
            }
            
            return memeVM;
        }

        public MemePagedListVM GetPagedMemes(int page, int itemsPerPage, System.Security.Claims.ClaimsPrincipal user)
        {
            var MemeList = new MemePagedListVM { PageCount = 1 };
            var MemesVM = new List<MemeVM>();
            List<Meme> memes = _applicationDbContext.Memes.Where(m => m.IsAccepted == true && m.IsArchived == false).OrderByDescending(m => m.AccpetanceDate).ToList();

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
                    Rate = _voteRepository.GetMemeRate(b.MemeId),
                    CommentCount = _applicationDbContext.Comments.Where(m => m.MemeRefId == b.MemeId).Count(),
                    IsAccepted = b.IsAccepted,
                    IsArchived = b.IsArchived,
                    IsVoted = false,
                    VoteValue = null,
                    IsFavourite = false,
                };

                if (user != null && user.Identity.IsAuthenticated == true)
                {
                    string userId = user.Claims.First(c => c.Type == "UserID").Value;
                    meme.IsVoted = _voteRepository.DidUserVote(meme.MemeId, userId);
                    meme.VoteValue = _applicationDbContext.Votes.FirstOrDefaultAsync(m => m.MemeRefId == meme.MemeId && m.UserId == userId).Result.Value;
                    meme.IsFavourite = false;
                }
                MemesVM.Add(meme);
            }
            MemeList.MemeList = MemesVM;
            return MemeList;
        }

        public MemePagedListVM GetPagedMemesByCategory(string category, int page, int itemsPerPage, System.Security.Claims.ClaimsPrincipal user)
        {
            var MemeList = new MemePagedListVM { PageCount = 1 };
            var MemesVM = new List<MemeVM>();
            var categoryModel = _applicationDbContext.Categories.FirstOrDefault(m => m.CategoryName == category);
            List<Meme> memes = _applicationDbContext.Memes.Where(m => m.CategoryId == categoryModel.CategoryId && m.IsArchived == false).OrderByDescending(m => m.CreationDate).ToList();
           
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
                    Rate = _voteRepository.GetMemeRate(b.MemeId),
                    CommentCount = _applicationDbContext.Comments.Where(m => m.MemeRefId == b.MemeId).Count(),
                    IsAccepted = b.IsAccepted,
                    IsArchived = b.IsArchived,
                    IsVoted = false,
                    VoteValue = null,
                    IsFavourite = false,
                };
                if (user != null && user.Identity.IsAuthenticated == true)
                {
                    string userId = user.Claims.First(c => c.Type == "UserID").Value;
                    meme.IsVoted = _voteRepository.DidUserVote(meme.MemeId, userId);
                    meme.VoteValue = _applicationDbContext.Votes.FirstOrDefaultAsync(m => m.MemeRefId == meme.MemeId && m.UserId == userId).Result.Value;
                    meme.IsFavourite = false;
                }
                MemesVM.Add(meme);
            }
            MemeList.MemeList = MemesVM;
            return MemeList;
        }

        public MemePagedListVM GetUnacceptedMemes(int page, int itemsPerPage, System.Security.Claims.ClaimsPrincipal user)
        {
            var MemeList = new MemePagedListVM { PageCount = 1 };
            var MemesVM = new List<MemeVM>();
            List<Meme> memes = _applicationDbContext.Memes.Where(m => m.IsAccepted == false && m.IsArchived == false).OrderByDescending(m => m.CreationDate).ToList(); ;

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
                    Rate = _voteRepository.GetMemeRate(b.MemeId),
                    CommentCount = _applicationDbContext.Comments.Where(m => m.MemeRefId == b.MemeId).Count(),
                    IsAccepted = b.IsAccepted,
                    IsArchived = b.IsArchived,
                    IsVoted = false,
                    VoteValue = null,
                    IsFavourite = false,
                };
                if (user != null && user.Identity.IsAuthenticated == true)
                {
                    string userId = user.Claims.First(c => c.Type == "UserID").Value;
                    meme.IsVoted = _voteRepository.DidUserVote(meme.MemeId, userId);
                    //meme.VoteValue = _applicationDbContext.Votes.FirstOrDefaultAsync(m => m.MemeRefId == meme.MemeId && m.UserId == userId).Result.Value;
                    meme.IsFavourite = false;
                }
                MemesVM.Add(meme);
            }
            MemeList.MemeList = MemesVM;
            return MemeList;
        }

        public MemePagedListVM GetArchivedMemes(int page, int itemsPerPage, System.Security.Claims.ClaimsPrincipal user)
        {
            var MemeList = new MemePagedListVM { PageCount = 1 };
            var MemesVM = new List<MemeVM>();
            List<Meme> memes = _applicationDbContext.Memes.Where(m => m.IsArchived == true).OrderByDescending(m => m.CreationDate).ToList(); ;

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
                    Rate = _voteRepository.GetMemeRate(b.MemeId),
                    CommentCount = _applicationDbContext.Comments.Where(m => m.MemeRefId == b.MemeId).Count(),
                    IsAccepted = b.IsAccepted,
                    IsArchived = b.IsArchived,
                    IsVoted = false,
                    VoteValue = null,
                    IsFavourite = false,
                };
                if (user != null && user.Identity.IsAuthenticated == true)
                {
                    string userId = user.Claims.First(c => c.Type == "UserID").Value;
                    meme.IsVoted = _voteRepository.DidUserVote(meme.MemeId, userId);
                    meme.VoteValue = _applicationDbContext.Votes.FirstOrDefaultAsync(m => m.MemeRefId == meme.MemeId && m.UserId == userId).Result.Value;
                    meme.IsFavourite = false;
                }
                MemesVM.Add(meme);
            }
            MemeList.MemeList = MemesVM;
            return MemeList;
        }

        public void ChangeAccpetanceStatus1(int memeId, bool value)
        {
            var meme = _applicationDbContext.Memes.FirstOrDefault(m => m.MemeId == memeId);
            if (value == true)
            {
                meme.AccpetanceDate = DateTime.Now;
                meme.IsAccepted = value;
                meme.IsArchived = false;
            }
            else
            {
                meme.AccpetanceDate = null;
            }
            _applicationDbContext.SaveChanges();
        }

        public void ChangeArchiveStatus1(int memeId, bool value)
        {
            var meme = _applicationDbContext.Memes.FirstOrDefault(m => m.MemeId == memeId);
            meme.IsArchived = value;
            _applicationDbContext.SaveChanges();
        }

        public async Task ChangeArchiveStatus(int memeId, bool value)
        {
            var meme = await _applicationDbContext.Memes.FirstOrDefaultAsync(m => m.MemeId == memeId);
            meme.IsArchived = value;
            await _applicationDbContext.SaveChangesAsync();
        }
        public async Task ChangeAccpetanceStatus(int memeId, bool value)
        {
            var meme = await _applicationDbContext.Memes.FirstOrDefaultAsync(m => m.MemeId == memeId);
            if (value == true)
            {
                meme.AccpetanceDate = DateTime.Now;
                meme.IsAccepted = value;
                meme.IsArchived = false;
            }
            else
            {
                meme.AccpetanceDate = null;
            }
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
