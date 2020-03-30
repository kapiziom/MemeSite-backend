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
        public bool IsArchivized { get; set; }
        public DateTime? AccpetanceDate { get; set; }
        public string Uri { get; set; }
        public string ImageName { get; set; }
        public string ByteHead { get; set; }
        public byte[] ImageByte { get; set; }
        public string UserID { get; set; }
        public int CategoryId { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Vote> Votes { get; set; }
    }
}
