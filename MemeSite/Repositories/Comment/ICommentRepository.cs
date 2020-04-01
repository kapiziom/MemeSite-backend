using MemeSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MemeSite.Repositories
{
    public interface ICommentRepository
    {
        List<CommentVM> GetCommentsAssignedToMeme(int memeId);
        void AddComment(AddCommentVM AddComment, string userId);
        bool EditComment(AddCommentVM comment, int id, string userId);
        bool DeleteEditComment(int id, string userId, string role);
    }
}
