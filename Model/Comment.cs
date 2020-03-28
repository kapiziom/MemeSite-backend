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
        [StringLength(1000, ErrorMessage = "The Comment must be at least {2} characters long.", MinimumLength = 2)]
        public string Txt { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? EditDate { get; set; }
        public bool IsDeleted { get; set; }

        public int MemeId { get; set; }
        public string UserID { get; set; }
        public PageUser PageUser { get; set; }
    }
}
