using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.ViewModels
{
    public class MemeDetails
    {
        public int MemeId { get; set; }
        public string Author { get; set; }
        public string CreationDate { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
    }
}
