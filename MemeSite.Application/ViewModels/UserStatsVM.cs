﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.Application.ViewModels
{
    public class UserStatsVM
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public int TotalMemes { get; set; }
        public int TotalAccepted { get; set; }
        public int TotalComments { get; set; }
        public string Joined { get; set; }
    }
}
