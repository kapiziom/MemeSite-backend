﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MemeSite.Model
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        [MaxLength(14, ErrorMessage = "Max category name length is 14 characters.")]
        public string CategoryName { get; set; }
        public ICollection<Meme> Memes { get; set; }
    }
}
