using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemeSite.Domain.Models
{
    public class Vote
    {
        public int VoteId { get; set; }
        public Value Value { get; set; }

        //foreign
        public string UserId { get; set; }
        public PageUser User { get; set; }
        public int MemeRefId { get; set; }
        public Meme Meme { get; set; }
    }
    

}
