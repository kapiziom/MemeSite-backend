using MemeSite.Model;
using MemeSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MemeSite.Repositories
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        Task<CommentVM> GetCommentVM(int id);
        List<CommentVM> GetCommentsAssignedToMeme(int memeId);
        Task<List<CommentVM>> GetCommentsAssignedToMeme1(int memeId);
        void AddComment(AddCommentVM AddComment, string userId);
        bool EditComment(AddCommentVM comment, int id, string userId);
        bool DeleteEditComment(int id, string userId, string role);
        int CommentCount(int memeId);


        Task<int> CountAsync(Expression<Func<Comment, bool>> filter);

    }
}
