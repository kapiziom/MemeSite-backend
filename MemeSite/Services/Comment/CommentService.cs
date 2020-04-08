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
    public class CommentService : GenericService<Comment>, ICommentService
    {
        private readonly UserManager<PageUser> _userManager;
        public CommentService(IGenericRepository<Comment> _commentRepository, UserManager<PageUser> userManager) : base(_commentRepository)
        {
            _userManager = userManager;
        }

        public async Task<PagedList<Comment>> GetPagedList<TKey>(Expression<Func<Comment, bool>> filter, Expression<Func<Comment, TKey>> order, int page, int itemsPerPage)
            => await _repository.GetPagedAsync(filter, order, page, itemsPerPage);

        

        public async Task<List<CommentVM>> GetListCommentVM(Expression<Func<Comment, bool>> filter)
        {
            var comments = await _repository.GetAllFilteredAsync(filter, m => m.CreationDate);
            List<CommentVM> commentsVM = new List<CommentVM>();
            foreach (var m in comments)
            {
                commentsVM.Add(await MapCommentVM(m));
            }
            return commentsVM;
        }

        

        public async Task<CommentVM> MapCommentVM(Comment comment)
        {
            var user = await _userManager.FindByIdAsync(comment.UserID);
            CommentVM vm = new CommentVM();
            vm.CommentId = comment.CommentId;
            vm.UserId = comment.UserID;
            vm.MemeId = comment.MemeRefId;
            vm.UserName = user.UserName;
            vm.Txt = comment.Txt;
            vm.CreationDate = comment.CreationDate.ToString("dd/MM/yyyy hh:mm");
            vm.EditDate = comment.EditDate?.ToString("dd/MM/yyyy hh:mm");
            return vm;
        }

        

        public async Task<CommentVM> GetCommentVM(int id)
        {
            var m = await _repository.FindAsync(id);//m is comment model
            if(m == null)
            {
                return null;
            }
            var VM = new CommentVM()
            {
                CommentId = m.CommentId,
                Txt = m.Txt,
                CreationDate = m.CreationDate.ToString("dd/MM/yyyy hh:mm"),
                EditDate = m.EditDate?.ToString("dd/MM/yyyy hh:mm"),
                UserName = _userManager.FindByIdAsync(m.UserID).Result.UserName,

                UserId = m.UserID,
                MemeId = m.MemeRefId,
            };
            return VM;

        }

        public async Task<bool> DeleteComment(int id, System.Security.Claims.ClaimsPrincipal user)
        {
            var comment = await _repository.FindAsync(id);
            if (user.Claims.First(c => c.Type == "UserID").Value == comment.UserID || 
                user.Claims.First(c => c.Type == "role").Value == "Administrator")
            {
                await _repository.DeleteAsync(comment);
                return true;
            }
            else return false;
        }

        public async Task InsertComment(AddCommentVM VM, string userId)
        {
            var comment = new Comment()
            {
                CreationDate = DateTime.Now,
                MemeRefId = VM.MemeId,
                Txt = VM.Txt,
                UserID = userId,
                IsDeleted = false,
                IsArchived = false,
            };
            await _repository.InsertAsync(comment);
        }

        public async Task<bool> UpdateComment(AddCommentVM VM, int id, string userId)
        {
            var comment = await _repository.FindAsync(id);
            if (comment.UserID == userId)
            {
                comment.Txt = comment.Txt;
                comment.EditDate = DateTime.Now;
                await _repository.UpdateAsync(comment);
                return true;
            }
            else return false;
        }

        public async Task<int> CommentCount(int memeId) => await _repository.CountAsync(m => m.MemeRefId == memeId);
    }
}