using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MemeSite.Model
{
    public class Vote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VoteId { get; set; }
        [Required]
        public int Value { get; set; }//-1 or 1

        [Required]
        public int UserId { get; set; }
        [Required]
        public int MemeId { get; set; }
    }
}
