using MemeSite.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MemeSite.ViewModels;
using MemeSite.Data;

namespace MemeSite.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CommentRepository(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }

        public void AddComment(AddCommentVM AddComment, string userId)
        {
            var user = _applicationDbContext.Users.FirstOrDefault(m => m.Id == userId);
            var comment = new Comment()
            {
                CreationDate = DateTime.Now,
                MemeId = AddComment.MemeId,
                Txt = AddComment.Txt,
                PageUser = user,
                UserID = user.Id,
                IsDeleted = false
            };
            _applicationDbContext.Comments.Add(comment);
            _applicationDbContext.SaveChanges();
        }
        public bool EditComment(AddCommentVM comment, int id, string userId)
        {
            var c = _applicationDbContext.Comments.FirstOrDefault(m => m.CommentId == id);
            if (c.UserID == userId)
            {
                c.Txt = comment.Txt;
                c.EditDate = DateTime.Now;
                _applicationDbContext.SaveChanges();
                return true;
            }
            else return false;
        }
        public bool DeleteEditComment(int id, string userId, string role)
        {
            var c = _applicationDbContext.Comments.FirstOrDefault(m => m.CommentId == id);
            if (c.UserID == userId || role != "NormalUser")
            {
                c.IsDeleted = true;
                _applicationDbContext.SaveChanges();
                return true;
            }
            else return false;
            
        }


    }
}
