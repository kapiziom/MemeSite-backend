using MemeSite.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.ViewModels
{
    public class MemeVM
    {
        public int MemeId { get; set; }
        public string UserName { get; set; }
        public string CreationDate { get; set; }
        public string Title { get; set; }
        public string ByteHead { get; set; }
        public byte[] ByteImg { get; set; }
        public CategoryVM Category { get; set; }
        public int Rate { get; set; }
        public int CommentCount { get; set; }
    }
}
