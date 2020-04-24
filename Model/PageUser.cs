using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.Model
{
    public class PageUser : IdentityUser
    {
        public DateTime CreationDate { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Meme> Memes { get; set; }
        public ICollection<Favourite> Favourites { get; set; }
    }
}
