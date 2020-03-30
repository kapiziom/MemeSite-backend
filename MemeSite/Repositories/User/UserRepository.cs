﻿using MemeSite.Data;
using MemeSite.Model;
using MemeSite.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MemeSite.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<PageUser> _userManager;
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(ApplicationDbContext context, UserManager<PageUser> userManager)
        {
            _applicationDbContext = context;
            _userManager = userManager;
        }

        

    }
}