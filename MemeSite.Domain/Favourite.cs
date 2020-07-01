using System;
using System.Collections.Generic;
using System.Text;

namespace MemeSite.Domain
{
    public class Favourite
    {
        //foreign
        public string UserId { get; set; }
        public PageUser User { get; set; }
        public int MemeRefId { get; set; }
        public Meme Meme { get; set; }

        public DateTime CreateFavDate { get; set; }
    }
}
