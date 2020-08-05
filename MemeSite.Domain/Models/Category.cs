using System;
using System.Collections.Generic;

namespace MemeSite.Domain.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public ICollection<Meme> Memes { get; set; }
    }
}
