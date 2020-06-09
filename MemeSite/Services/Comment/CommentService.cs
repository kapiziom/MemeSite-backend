using AutoMapper;
using FluentValidation;
using MemeSite.Data.Models;
using MemeSite.Data.Models.Common;
using MemeSite.Data.Models.Exceptions;
using MemeSite.Data.Repository;
using MemeSite.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace MemeSite.Services
{
    public class CommentService : GenericService<Comment>, ICommentService
    {
        private readonly UserManager<PageUser> _userManager;
        private readonly IMapper _mapper;
        public CommentService(
            IGenericRepository<Comment> _commentRepository,
            IValidator<Comment> validator,
            UserManager<PageUser> userManager,
            IMapper mapper) : base(_commentRepository, validator)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<PagedList<Comment>> GetPagedList<TKey>(Expression<Func<Comment, bool>> filter, Expression<Func<Comment, TKey>> order, int page, int itemsPerPage)
            => await _repository.GetPagedAsync(filter, order, page, itemsPerPage);

        public async Task<PagedList<CommentVM>> GetPagedListVM<TKey>
            (Expression<Func<Comment, bool>> filter, 
            Expression<Func<Comment, TKey>> order, 
            int page, int itemsPerPage)
        {
            var model = await _repository.GetPagedAsync(filter, order, page, itemsPerPage, x => x.PageUser);
            var VM = new PagedList<CommentVM>();
            VM.ItemsPerPage = model.ItemsPerPage;
            VM.Page = model.Page;
            VM.PageCount = model.PageCount;
            VM.TotalItems = model.TotalItems;
            
            VM.Items = _mapper.Map<List<CommentVM>>(model.Items);

            return VM;
        }

        public async Task<List<CommentVM>> GetListCommentVM(Expression<Func<Comment, bool>> filter)
        {
            var query = _repository.Query().Where(filter).Include(x => x.PageUser);

            var comments = await query.OrderByDescending(m => m.CreationDate).ToListAsync();

            return _mapper.Map<List<CommentVM>>(comments);
        }

        public async Task<CommentVM> GetCommentVM(int id)
        {
            var comment = await _repository.FindAsync(m => m.CommentId == id, x => x.PageUser);

            if (comment == null)
                throw new MemeSiteException(HttpStatusCode.NotFound, "Comment not found.");

            return _mapper.Map<CommentVM>(comment);

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

            return _mapper.Map<CommentVM>(comment);
        }

        public async Task<CommentVM> InsertComment(AddCommentVM VM, string userId)
        {
            VM.UserId = userId;
            var comment = _mapper.Map<Comment>(VM);

            var result = Validate(comment);
            if (result.Succeeded) 
            {
                return _mapper.Map<CommentVM>(await _repository.InsertAsync(comment));
            } 
            else throw new MemeSiteException(HttpStatusCode.BadRequest, "Validation failed", result);
        }

        public async Task<CommentVM> UpdateComment(EditCommentVM VM, int id, string userId)
        {
            var comment = await _repository.FindAsync(m => m.CommentId == id, x => x.PageUser);

            if (comment == null)
                throw new MemeSiteException(HttpStatusCode.NotFound, "Comment not found");

            if (comment.UserID == userId && comment.IsArchived == false)
            {
                comment.LastTxt = comment.Txt;
                comment.Txt = VM.Txt;
                comment.EditDate = DateTime.Now;
                var result = await ValidateAsync(comment);
                if (result.Succeeded)
                {
                    await _repository.UpdateAsync(comment);
                    return _mapper.Map<CommentVM>(comment);
                }

                else throw new MemeSiteException(HttpStatusCode.BadRequest, null, result);

            }
            else throw new MemeSiteException(HttpStatusCode.Forbidden, "You don't have permission to edit this");
        }

        public async Task<int> CommentCount(int memeId) => await _repository.CountAsync(m => m.MemeRefId == memeId);

        

    }
}
