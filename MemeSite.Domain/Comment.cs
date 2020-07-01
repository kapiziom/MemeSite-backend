using System;

namespace MemeSite.Domain
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Txt { get; set; }
        public string LastTxt { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? EditDate { get; set; }
        public bool IsArchived { get; set; }


        //foreign
        public int MemeRefId { get; set; }
        public Meme Meme { get; set; }
        public string UserID { get; set; }
        public PageUser PageUser { get; set; }


    }
}
