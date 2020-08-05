using MemeSite.Data.DbContext;
using MemeSite.Domain.Interfaces;
using MemeSite.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MemeSite.Data.Repository
{
    public class VoteRepository : GenericRepository<Vote>, IVoteRepository
    {
        public VoteRepository(ApplicationDbContext context)
            : base(context)
        { }
    }
}
