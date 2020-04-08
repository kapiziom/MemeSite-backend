﻿using MemeSite.Model;
using MemeSite.Repository;
using MemeSite.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MemeSite.Services
{
    public class MemeService : GenericService<Meme>, IMemeService
    {
        private readonly UserManager<PageUser> _userManager;
        private readonly ICategoryService _categoryService;
        private readonly ICommentService _commentService;
        private readonly IVoteService _voteService;
        public MemeService(IGenericRepository<Meme> _memeRepository,
            UserManager<PageUser> userManager,
            ICategoryService categoryService,
            ICommentService commentService,
            IVoteService voteService) : base(_memeRepository)
        {
            _userManager = userManager;
            _categoryService = categoryService;
            _commentService = commentService;
            _voteService = voteService;
        }

        public async Task Upload(MemeUploadVM model, string userId)
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
            await _repository.InsertAsync(meme);
        }

        public async Task<MemeDetailsVM> GetMemeDetailsById(int id, System.Security.Claims.ClaimsPrincipal user)
        {
            var entity = await _repository.FindAsync(id);
            if (entity == null)
            {
                return null;
            }
            var memeVM = new MemeDetailsVM()
            {
                MemeId = entity.MemeId,
                Title = entity.Title,
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
                memeVM.IsFavourite = false;//to implement
            }
            return memeVM;
        }

        public async Task<PagedList<MemeVM>> GetPagedMemesAsync<TKey>(
            Expression<Func<Meme, bool>> filter, 
            Expression<Func<Meme, TKey>> order, 
            int page, int itemsPerPage, 
            System.Security.Claims.ClaimsPrincipal user)
        {
            var model = await _repository.GetPagedAsync(filter, order, page, itemsPerPage);
            var VM = new PagedList<MemeVM>();
            VM.ItemsPerPage = model.ItemsPerPage;
            VM.Page = model.Page;
            VM.PageCount = model.PageCount;
            VM.TotalItems = model.TotalItems;

            List<MemeVM> list = new List<MemeVM>();

            foreach (var m in model.Items)
            {
                list.Add(await CustomMapMemeVM(m, user));
            }
            VM.Items = list;
            return VM;
        }

        public async Task<bool> DeleteMeme(int id, System.Security.Claims.ClaimsPrincipal user)
        {
            var meme = await _repository.FindAsync(id);
            if (user.Claims.First(c => c.Type == "UserID").Value == meme.UserID && meme != null)
            {
                await _repository.DeleteAsync(meme);
                return true;
            }
            return false;
        }

        public async Task ChangeArchiveStatus(int memeId, bool value)
        {
            var entity = await _repository.FindAsync(memeId);
            entity.IsArchived = value;
            await _repository.UpdateAsync(entity);
        }
        public async Task ChangeAccpetanceStatus(int memeId, bool value)
        {
            var entity = await _repository.FindAsync(memeId);
            if (value == true)
            {
                entity.AccpetanceDate = DateTime.Now;
                entity.IsAccepted = value;
                entity.IsArchived = false;
            }
            else
            {
                entity.AccpetanceDate = null;
            }
            await _repository.UpdateAsync(entity);
        }

        public async Task<int> Count(Expression<Func<Meme, bool>> filter) => await _repository.CountAsync(filter);



        public async Task<MemeVM> CustomMapMemeVM(Meme entity, System.Security.Claims.ClaimsPrincipal user)
        {
            MemeVM vm = new MemeVM();
            vm.MemeId = entity.MemeId;
            vm.Title = entity.Title;
            vm.UserId = entity.UserID;
            vm.UserName =  _userManager.FindByIdAsync(entity.UserID).Result.UserName;
            vm.ByteHead = entity.ByteHead;
            vm.ByteImg = entity.ImageByte;
            vm.Category = await _categoryService.GetCategoryVM(entity.CategoryId);
            vm.CreationDate = entity.CreationDate.ToString("dd/MM/yyyy");
            vm.Rate = await _voteService.GetMemeRate(entity.MemeId);
            vm.CommentCount = await _commentService.CountAsync(m => m.MemeRefId == entity.MemeId);
            vm.IsAccepted = entity.IsAccepted;
            vm.IsArchived = entity.IsArchived;
            vm.IsVoted = false;
            vm.IsFavourite = false;
            vm.VoteValue = null;
            if (user != null && user.Identity.IsAuthenticated == true)
            {
                string userId = user.Claims.First(c => c.Type == "UserID").Value;
                vm.IsVoted = await _voteService.IsExistAsync(m => m.MemeRefId == entity.MemeId && m.UserId == userId);
                vm.VoteValue = await _voteService.GetValueIfExist(entity.MemeId, userId);
                vm.IsFavourite = false;//to implement
            }
            return vm;
        }
    }
}