using System;
using System.Collections.Generic;
using System.Text;
using MemeSite.Data;
using MemeSite.Model;


namespace MemeSite.Repositories
{
    public class VoteRepository : IVoteRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public VoteRepository(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }
    }
}
