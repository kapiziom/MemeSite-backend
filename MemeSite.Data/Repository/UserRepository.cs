using MemeSite.Data.DbContext;
using MemeSite.Domain.Interfaces;
using MemeSite.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MemeSite.Data.Repository
{
    public class UserRepository : GenericRepository<PageUser>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context)
            : base(context)
        { }
    }
}
