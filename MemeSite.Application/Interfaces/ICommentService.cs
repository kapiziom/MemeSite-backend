using MemeSite.Application.ViewModels;
using MemeSite.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MemeSite.Application.Interfaces
{
    public interface ICommentService : IGenericService<Comment>
    {
        Task<CommentVM> GetCommentVM(int id);
        Task<List<CommentVM>> GetListCommentVM(Expression<Func<Comment, bool>> filter);
        Task DeleteComment(int id, System.Security.Claims.ClaimsPrincipal user);
        Task<CommentVM> ArchiveTxtAsAdmin(int id);//change txt to [deleted by admin]
        Task<CommentVM> InsertComment(AddCommentVM VM, string userId);
        Task<CommentVM> UpdateComment(EditCommentVM VM, int id, string userId);
        Task<int> CommentCount(int memeId);

        Task<PagedList<Comment>> GetPagedList<TKey>(Expression<Func<Comment, bool>> filter, Expression<Func<Comment, TKey>> order, int page, int itemsPerPage);
        Task<PagedList<CommentVM>> GetPagedListVM<TKey>(Expression<Func<Comment, bool>> filter, Expression<Func<Comment, TKey>> order, int page, int itemsPerPage);

    }
}
