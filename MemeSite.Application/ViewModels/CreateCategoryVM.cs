using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.Application.ViewModels
{
    public class CreateCategoryVM
    {
        [Required]
        [MaxLength(14, ErrorMessage = "Max category name length is 14 characters.")]
        public string CategoryName { get; set; }
    }
}
