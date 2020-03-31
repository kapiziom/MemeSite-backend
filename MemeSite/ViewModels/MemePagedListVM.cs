using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.ViewModels
{
    public class MemePagedListVM
    {
        public List<MemeVM> MemeList { get; set; }
        public int PageCount { get; set; }
    }
}
