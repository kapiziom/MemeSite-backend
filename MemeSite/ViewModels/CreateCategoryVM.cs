using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.ViewModels
{
    public class CreateCategoryVM
    {
        [Required]
        public string CategoryName { get; set; }
    }
}
