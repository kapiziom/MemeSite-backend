using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MemeSite.Model
{
    public class Meme
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MemeId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Txt { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsArchived { get; set; }
        public DateTime? AccpetanceDate { get; set; }
        public string Uri { get; set; }
        public string ImageName { get; set; }
        public string ByteHead { get; set; }
        public byte[] ImageByte { get; set; }

        [ForeignKey("PageUser")]
        public string UserID { get; set; }
        public PageUser PageUser { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Vote> Votes { get; set; }
        public ICollection<Favourite> Favourites { get; set; }
    }
}
