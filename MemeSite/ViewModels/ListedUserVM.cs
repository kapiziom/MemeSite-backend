using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.ViewModels
{
    public class ListedUserVM
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string CreationDate { get; set; }
        public int MemeCount { get; set; }
        public int CommentCount { get; set; }
        public string UserRole { get; set; }
    }
}
