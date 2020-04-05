using MemeSite.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MemeSite.Services
{
    public interface ICommentService : IGenericService<Comment>
    {
        Task<int> Count(Expression<Func<Comment, bool>> filter);

        Task<Comment> GetById(int id);
    }
}
