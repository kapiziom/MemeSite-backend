using FluentValidation;
using MemeSite.Data.Models;
using MemeSite.Data.Models.Common;
using MemeSite.Data.Models.Exceptions;
using MemeSite.Data.Repository;
using MemeSite.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace MemeSite.Services
{
    public class MemeService : GenericService<Meme>, IMemeService
    {
        private readonly UserManager<PageUser> _userManager;
        private readonly ICategoryService _categoryService;
        private readonly ICommentService _commentService;
        private readonly IVoteService _voteService;
        private readonly IFavouriteService _favouriteService;
        public MemeService(IGenericRepository<Meme> _memeRepository,
            IValidator<Meme> validator,
            UserManager<PageUser> userManager,
            ICategoryService categoryService,
            ICommentService commentService,
            IVoteService voteService,
            IFavouriteService favouriteService) : base(_memeRepository, validator)
        {
            _userManager = userManager;
            _categoryService = categoryService;
            _commentService = commentService;
            _voteService = voteService;
            _favouriteService = favouriteService;
        }

        public async Task<Result<Meme>> Upload(MemeUploadVM model, string userId)
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
            return await Insert(meme);
        }


        public async Task<MemeDetailsVM> GetMemeDetailsById(int id, System.Security.Claims.ClaimsPrincipal user)
        {
            var entity = await _repository.FindAsync(id);
            if (entity == null)
                throw new MemeSiteException(HttpStatusCode.NotFound, "Not Found");
            var memeVM = new MemeDetailsVM()
            {
                MemeId = entity.MemeId,
                Title = entity.Title,
                Txt = entity.Txt,
                UserId = entity.UserID,
                UserName = _userManager.FindByIdAsync(entity.UserID).Result.UserName,
                ByteHead = entity.ByteHead,
                ByteImg = entity.ImageByte,
                Category = await _categoryService.GetCategoryVM(entity.CategoryId),
                CreationDate = entity.CreationDate.ToString("dd/MM/yyyy"),
                Rate = await _voteService.GetMemeRate(entity.MemeId),
                CommentCount = await _commentService.CountAsync(m => m.MemeRefId == entity.MemeId),
                IsAccepted = entity.IsAccepted,
                IsArchived = entity.IsArchived,
                IsVoted = false,
                IsFavourite = false,
                VoteValue = null,
            };
            if (user != null && user.Identity.IsAuthenticated == true)
            {
                string userId = user.Claims.First(c => c.Type == "UserID").Value;
                memeVM.IsVoted = await _voteService.IsExistAsync(m => m.MemeRefId == id && m.UserId == userId);
                memeVM.VoteValue = await _voteService.GetValueIfExist(entity.MemeId, userId);
                memeVM.IsFavourite = await _favouriteService.IsExistAsync(m => m.MemeRefId == id && m.UserId == userId);
            }
            return memeVM;
        }

        public async Task<PagedList<MemeVM>> GetPagedMemesAsync<TKey>(
            Expression<Func<Meme, bool>> filter, 
            Expression<Func<Meme, TKey>> order, 
            int page, int itemsPerPage, 
            System.Security.Claims.ClaimsPrincipal user)
        {
            var model = await _repository.GetPagedAsync(filter, order, page, itemsPerPage,
                x => x.Comments, x => x.Votes, x => x.Favourites, x => x.PageUser, x => x.Category);
            var VM = new PagedList<MemeVM>();
            VM.ItemsPerPage = model.ItemsPerPage;
            VM.Page = model.Page;
            VM.PageCount = model.PageCount;
            VM.TotalItems = model.TotalItems;

            List<MemeVM> list = new List<MemeVM>();

            foreach (var m in model.Items)
            {
                list.Add(await MapMemeVM(m, user));
            }
            VM.Items = list;
            return VM;
        }

        public async Task<PagedList<MemeVM>> GetPagedFavouritesMemesAsync
            (int page, int itemsPerPage, System.Security.Claims.ClaimsPrincipal user)
        {
            string userId = user.Claims.First(c => c.Type == "UserID").Value;
            var resList = new PagedList<MemeVM>(); //zwracany model
            List<MemeVM> list = new List<MemeVM>();// PagedList.Items
            var favourites = await _favouriteService.GetAllAsync(m => m.UserId == userId);
            var favs = await _favouriteService.GetAllFilteredIncludeAsync(m => m.UserId == userId,
                x => x.MemeRefId);
            
            foreach (var m in favourites)
            {
                list.Add(await MapMemeVM(await _repository.FindAsync(m.MemeRefId), user));
            }
            

            //available pages
            resList.PageCount = (int)Math.Ceiling(((double)list.Count() / itemsPerPage));

            list = list.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList();

            resList.ItemsPerPage = itemsPerPage;
            resList.Page = page;
            resList.TotalItems = list.Count();
            resList.Items = list;
            return resList;
        }

        public async Task<object> GetUsersFavourites(System.Security.Claims.ClaimsPrincipal user)
        {
            string userId = user.Claims.First(c => c.Type == "UserID").Value;
            var favs = await _favouriteService.GetAllFilteredIncludeAsync(m => m.UserId == userId,
                x => x.Meme.Comments,
                x => x.Meme.Favourites,
                x => x.Meme.Votes,
                x => x.Meme.PageUser,
                x => x.Meme.Category);
            List<MemeVM> listVM = new List<MemeVM>();
            foreach (var fav in favs)
            {
                listVM.Add(await MapMemeVM(fav.Meme, user));
            }
            var result = listVM;
            return listVM;
        }

        public async Task<PagedList<MemeVM>> GetPagedUsersFavourites(int page, int itemsPerPage, System.Security.Claims.ClaimsPrincipal user)
        {
            string userId = user.Claims.First(c => c.Type == "UserID").Value;
            var MemeList = await _favouriteService.GetUsersFavourites(userId);
            List<MemeVM> listVM = new List<MemeVM>();
            var resList = new PagedList<MemeVM>();

            foreach (var m in MemeList)
            {
                listVM.Add(await MapMemeVM(m, user));
            }

            //available pages
            resList.PageCount = (int)Math.Ceiling(((double)listVM.Count() / itemsPerPage));

            listVM = listVM.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList();

            resList.ItemsPerPage = itemsPerPage;
            resList.Page = page;
            resList.TotalItems = listVM.Count();
            resList.Items = listVM;
            return resList;
        }

        public async Task DeleteMeme(int id, System.Security.Claims.ClaimsPrincipal user)
        {
            var meme = await _repository.FindAsync(id);
            if (meme == null)
                throw new MemeSiteException(HttpStatusCode.NotFound, "Item not Found");
            if (user.Claims.First(c => c.Type == "UserID").Value == meme.UserID ||
                user.Claims.First(c => c.Type == "userRole").Value == "Administrator")
            {
                await _repository.DeleteAsync(meme);
            }
            throw new MemeSiteException(HttpStatusCode.Forbidden, "You don't have permission to delete this");
        }

        public async Task ChangeArchiveStatus(int memeId, bool value)
        {
            var entity = await _repository.FindAsync(memeId);
            if(entity == null)
                throw new MemeSiteException(HttpStatusCode.NotFound, "Item not Found");
            entity.IsArchived = value;
            await _repository.UpdateAsync(entity);
        }
        public async Task ChangeAccpetanceStatus(int memeId, bool value)
        {
            var entity = await _repository.FindAsync(memeId);
            if (entity == null)
                throw new MemeSiteException(HttpStatusCode.NotFound, "Item not Found");
            if (value == true)
            {
                entity.AccpetanceDate = DateTime.Now;
                entity.IsAccepted = value;
                entity.IsArchived = false;
            }
            else
            {
                entity.IsAccepted = value;
                entity.AccpetanceDate = null;
            }
            await _repository.UpdateAsync(entity);
        }

        public async Task<Result<Meme>> EditMeme(EditMemeVM meme, int id, System.Security.Claims.ClaimsPrincipal user)
        {
            var entity = await _repository.FindAsync(id);
            if (entity == null)
                throw new MemeSiteException(HttpStatusCode.NotFound, "Item not Found");
            if (entity.UserID == user.Claims.First(c => c.Type == "UserID").Value)
            {
                entity.Title = meme.Title;
                entity.Txt = meme.Txt;
                entity.CategoryId = meme.CategoryId;
                return await Update(entity);
            }
            else throw new MemeSiteException(HttpStatusCode.Forbidden, "You don't have permission to edit this");
        }

        public async Task<MemeVM> MapMemeVM(Meme entity, System.Security.Claims.ClaimsPrincipal user)
        {
            MemeVM vm = new MemeVM();
            vm.MemeId = entity.MemeId;
            vm.Title = entity.Title;
            vm.UserId = entity.UserID;
            vm.UserName = entity.PageUser.UserName;
            vm.ByteHead = entity.ByteHead;
            vm.ByteImg = entity.ImageByte;
            vm.Category = await _categoryService.GetCategoryVM(entity.CategoryId);
            vm.CreationDate = entity.CreationDate.ToString("dd/MM/yyyy");
            vm.Rate = entity.Rate();
            vm.CommentCount = entity.CommentCount();
            vm.IsAccepted = entity.IsAccepted;
            vm.IsArchived = entity.IsArchived;
            vm.IsVoted = false;
            vm.IsFavourite = false;
            vm.VoteValue = null;
            if (user != null && user.Identity.IsAuthenticated == true)
            {
                string userId = user.Claims.First(c => c.Type == "UserID").Value;
                vm.IsVoted = entity.IsVoted(userId);
                vm.VoteValue = entity.VoteValue(userId);
                vm.IsFavourite = entity.IsFavourite(userId);
            }
            return vm;
        }
    }
}
