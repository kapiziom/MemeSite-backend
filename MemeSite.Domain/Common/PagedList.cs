using System;
using System.Collections.Generic;
using System.Text;

namespace MemeSite.Domain.Common
{
    public class PagedList<T>
    {
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
        public int PageCount { get; set; }
        public int TotalItems { get; set; }
        public IList<T> Items { get; set; }

    }
}
