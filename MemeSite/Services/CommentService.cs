using MemeSite.Model;
using MemeSite.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MemeSite.Services
{
    public class CommentService : GenericService<Comment>, ICommentService
    {

        public CommentService(ICommentRepository _commentRepository) : base(_commentRepository) { }


        public async Task<int> Count(Expression<Func<Comment, bool>> filter) => await _repository.CountAsync(filter);
        //_commentRepository.CountAsync(filter);
        public async Task<Comment> GetById(int id)
        {
            var model = await _repository.FindAsync(id);
            return model;
        }
        //public async Task<int> Count(Expression<Func<Comment, bool>> filter) => await _commentRepository.CountAsync(filter);
    }
}
