﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.Application.ViewModels
{
    public class EditCommentVM
    {
        [Required]
        [StringLength(2000, ErrorMessage = "The Comment must be at least {2} characters long.", MinimumLength = 3)]
        public string Txt { get; set; }
    }
}
