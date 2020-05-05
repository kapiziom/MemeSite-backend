using MemeSite.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace MemeSite.Data.Models
{
    public class Meme
    {
        public int MemeId { get; set; }
        public string Title { get; set; }
        public string Txt { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public bool IsAccepted { get; set; } = false;
        public bool IsArchived { get; set; } = false;
        public DateTime? AccpetanceDate { get; set; }
        public string Uri { get; set; }
        public string ImageName { get; set; }
        public string ByteHead { get; set; }
        public byte[] ImageByte { get; set; }


        //foreign
        public string UserID { get; set; }
        public PageUser PageUser { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Vote> Votes { get; set; }
        public ICollection<Favourite> Favourites { get; set; }


        public int Rate()
        {
            var upvotes = Votes?.Where(m => m.Value == Value.upvote).Count() ?? 0;
            var downvotes = Votes?.Where(m => m.Value == Value.downvote).Count() ?? 0;
            return upvotes - downvotes;
        }
        public int CommentCount() => Comments?.Count ?? 0;
        //for logged in user
        public bool IsFavourite(string currentUserId) =>
            Favourites?.FirstOrDefault(m => m.UserId == currentUserId) != null;
        public bool IsVoted(string currentUserId) =>
            Votes?.FirstOrDefault(m => m.UserId == currentUserId) != null;
        public Value? VoteValue(string currentUserId) =>
            Votes?.FirstOrDefault(m => m.UserId == currentUserId)?.Value;

    }
}
