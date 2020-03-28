﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.ViewModels
{
    public class EditCommentVM
    {
        [Required]
        public int CommentId { get; set; }
        [Required]
        [StringLength(1000, ErrorMessage = "The Comment must be at least {2} characters long.", MinimumLength = 2)]
        public string Txt { get; set; }
        [Required]
        public int MemeId { get; set; }
    }
}
