using MemeSite.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MemeSite.ViewModels;
using MemeSite.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MemeSite.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        private readonly IUserRepository _userRepository;

        public CommentRepository(ApplicationDbContext _applicationDbContext, IUserRepository userRepository) : base(_applicationDbContext)
        {
            _userRepository = userRepository;
        }

        public List<CommentVM> GetCommentsAssignedToMeme(int memeId)
        {
            var comments = _applicationDbContext.Comments.Where(m => m.MemeRefId == memeId).OrderByDescending(m => m.CreationDate).ToList();
            List<CommentVM> commentsVM = new List<CommentVM>();
            foreach(var m in comments)
            {
                var comment = new CommentVM()
                {
                    CommentId = m.CommentId,
                    Txt = m.Txt,
                    CreationDate = m.CreationDate.ToString("dd/MM/yyyy hh:mm"),
                    EditDate = m.EditDate?.ToString("dd/MM/yyyy hh:mm"),
                    UserName = _userRepository.GetUsernameById(m.UserID),
                };
                commentsVM.Add(comment);
            }
            return commentsVM;
        }

        public async Task<List<CommentVM>> GetCommentsAssignedToMeme1(int memeId)
        {
            var comments = await _applicationDbContext.Comments.Where(m => m.MemeRefId == memeId).OrderByDescending(m => m.CreationDate).ToListAsync();
            List<CommentVM> commentsVM = new List<CommentVM>();
            foreach (var m in comments)
            {
                var comment = new CommentVM()
                {
                    CommentId = m.CommentId,
                    Txt = m.Txt,
                    CreationDate = m.CreationDate.ToString("dd/MM/yyyy hh:mm"),
                    EditDate = m.EditDate?.ToString("dd/MM/yyyy hh:mm"),
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
                IsDeleted = false,
                IsArchived = false,
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

        public async Task<CommentVM> GetCommentVM(int id)
        {
            var m = await _applicationDbContext.Comments.FirstOrDefaultAsync(model => model.CommentId == id);
            if (m == null)
            {
                return null;
            }
            var comment = new CommentVM()
            {
                CommentId = m.CommentId,
                Txt = m.Txt,
                CreationDate = m.CreationDate.ToString("dd/MM/yyyy hh:mm"),
                EditDate = m.EditDate?.ToString("dd/MM/yyyy hh:mm"),
                UserName = _userRepository.GetUsernameById(m.UserID),
            };
            return comment;
        }

        public int CommentCount(int memeId)
        {
            return _applicationDbContext.Comments.Where(m => m.MemeRefId == memeId).Count();
        }



        public async Task<int> CountAsync(Expression<Func<Comment, bool>> filter) => await _applicationDbContext.Comments.CountAsync(filter);

    }
}
