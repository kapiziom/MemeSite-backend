using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MemeSite.Model
{
    public class Favourite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FavId { get; set; }
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public PageUser User { get; set; }

        [Required]
        [ForeignKey("Meme")]
        public int MemeRefId { get; set; }
        public Meme Meme { get; set; }
    }
}
