﻿using MemeSite.Model;
using MemeSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MemeSite.Services
{
    public interface ICommentService : IGenericService<Comment>
    {
        Task<CommentVM> GetCommentVM(int id);
        Task<List<CommentVM>> GetListCommentVM(Expression<Func<Comment, bool>> filter);
        Task<bool> DeleteComment(int id, System.Security.Claims.ClaimsPrincipal user);
        Task InsertComment(AddCommentVM VM, string userId);
        Task<bool> UpdateComment(AddCommentVM VM, int id, string userId);
        Task<int> CommentCount(int memeId);

        Task<PagedList<Comment>> GetPagedList<TKey>(Expression<Func<Comment, bool>> filter, Expression<Func<Comment, TKey>> order, int page, int itemsPerPage);
    }
}