using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.Application.ViewModels
{
    public class ChangeEmailVM
    {
        [Required]
        [EmailAddress]
        public string NewEmail { get; set; }
    }
}
