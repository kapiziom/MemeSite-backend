using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MemeSite.Model
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }
        [Required]
        [StringLength(2000, ErrorMessage = "The Comment must be at least {2} characters long.", MinimumLength = 3)]
        public string Txt { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? EditDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsArchived { get; set; }

        [ForeignKey("Meme")]
        public int MemeRefId { get; set; }
        public Meme Meme { get; set; }

        [ForeignKey("PageUser")]
        public string UserID { get; set; }
        public PageUser PageUser { get; set; }

    }
}
