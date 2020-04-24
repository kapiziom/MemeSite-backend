using FluentValidation;
using MemeSite.Data.Models;
using MemeSite.Data.Models.Common;
using MemeSite.Data.Models.Exceptions;
using MemeSite.Data.Repository;
using MemeSite.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace MemeSite.Services
{
    public class CommentService : GenericService<Comment>, ICommentService
    {
        private readonly UserManager<PageUser> _userManager;
        public CommentService(
            IGenericRepository<Comment> _commentRepository,
            IValidator<Comment> validator,
            UserManager<PageUser> userManager) : base(_commentRepository, validator)
        {
            _userManager = userManager;
        }

        public async Task<PagedList<Comment>> GetPagedList<TKey>(Expression<Func<Comment, bool>> filter, Expression<Func<Comment, TKey>> order, int page, int itemsPerPage)
            => await _repository.GetPagedAsync(filter, order, page, itemsPerPage);

        public async Task<PagedList<CommentVM>> GetPagedListVM<TKey>
            (Expression<Func<Comment, bool>> filter, 
            Expression<Func<Comment, TKey>> order, 
            int page, int itemsPerPage)
        {
            var model = await _repository.GetPagedAsync(filter, order, page, itemsPerPage);
            var VM = new PagedList<CommentVM>();
            VM.ItemsPerPage = model.ItemsPerPage;
            VM.Page = model.Page;
            VM.PageCount = model.PageCount;
            VM.TotalItems = model.TotalItems;
            List<CommentVM> list = new List<CommentVM>();

            foreach (var m in model.Items)
            {
                list.Add(await MapCommentVM(m));
            }
            VM.Items = list;
            return VM;
        }

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

        public async Task<CommentVM> GetCommentVM(int id)
        {
            var m = await _repository.FindAsync(id);
            if (m == null)
                throw new MemeSiteException(HttpStatusCode.NotFound, "Comment not found.");
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

        public async Task DeleteComment(int id, System.Security.Claims.ClaimsPrincipal user)
        {
            var comment = await _repository.FindAsync(id);
            if (user.Claims.First(c => c.Type == "UserID").Value == comment.UserID ||
                user.Claims.First(c => c.Type == "userRole").Value == "Administrator")
            {
                await _repository.DeleteAsync(comment);
            }
            else throw new MemeSiteException(HttpStatusCode.Forbidden, "You don't have permission to delete this");
        }

        public async Task<CommentVM> ArchiveTxtAsAdmin(int id)
        {
            var comment = await _repository.FindAsync(id);
            comment.LastTxt = comment.Txt;
            comment.IsArchived = true;
            comment.Txt = "[Deleted by admin]";
            await _repository.UpdateAsync(comment);
            var model = await MapCommentVM(comment);
            return model;
        }

        public async Task<CommentVM> InsertComment(AddCommentVM VM, string userId)
        {
            var comment = new Comment()
            {
                CreationDate = DateTime.Now,
                MemeRefId = VM.MemeId,
                Txt = VM.Txt,
                UserID = userId,
                IsArchived = false,
            };
            var result = Validate(comment);
            if (result.Succeeded) 
            {
                return await MapCommentVM(await _repository.InsertAsync(comment));
            } 
            else throw new MemeSiteException(HttpStatusCode.BadRequest, "Validation failed", result);
        }

        public async Task<CommentVM> UpdateComment(EditCommentVM VM, int id, string userId)
        {
            var comment = await _repository.FindAsync(id);
            if (comment == null)
                throw new MemeSiteException(HttpStatusCode.NotFound, "Comment not found");
            if (comment.UserID == userId && comment.IsArchived == false)
            {
                comment.LastTxt = comment.Txt;
                comment.Txt = VM.Txt;
                comment.EditDate = DateTime.Now;
                var result = await ValidateAsync(comment);
                if(result.Succeeded)
                {
                    await _repository.UpdateAsync(comment);
                }
                
                var vm = await MapCommentVM(comment);
                return vm;
            }
            else throw new MemeSiteException(HttpStatusCode.Forbidden, "You don't have permission to edit this");
        }

        public async Task<int> CommentCount(int memeId) => await _repository.CountAsync(m => m.MemeRefId == memeId);

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

    }
}
