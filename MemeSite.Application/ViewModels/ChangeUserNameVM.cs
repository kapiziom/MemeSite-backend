using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.Application.ViewModels
{
    public class ChangeUserNameVM
    {
        [Required]
        [StringLength(16, ErrorMessage = "The {0} must be at least {2} characters long and maximum {1} characters", MinimumLength = 3)]
        public string NewUserName { get; set; }
    }
}
