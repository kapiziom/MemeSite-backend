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
        private readonly IUserRepository _userRepository;

        public CommentRepository(ApplicationDbContext context, IUserRepository userRepository)
        {
            _applicationDbContext = context;
            _userRepository = userRepository;
        }

        public List<CommentVM> GetCommentsAssignedToMeme(int memeId)
        {
            var comments = _applicationDbContext.Comments.Where(m => m.MemeRefId == memeId);
            List<CommentVM> commentsVM = new List<CommentVM>();
            foreach(var m in comments)
            {
                var comment = new CommentVM()
                {
                    CommentId = m.CommentId,
                    Txt = m.Txt,
                    CreationDate = m.CreationDate.ToString("dd/MM/yyyy"),
                    EditDate = m.EditDate?.ToString("dd/MM/yyyy"),
                    UserName = _userRepository.GetUsernameById(m.UserID),
                };
                commentsVM.Add(comment);
            }
            return commentsVM;
        }

        public void AddComment(AddCommentVM AddComment, string userId)
        {
            var user = _applicationDbContext.Users.FirstOrDefault(m => m.Id == userId);
            var comment = new Comment()
            {
                CreationDate = DateTime.Now,
                MemeRefId = AddComment.MemeId,
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
