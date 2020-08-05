using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.Application.ViewModels
{
    public class SetUserRoleVM
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        [AvailableRoles]
        public string Role { get; set; }
    }

    public class AvailableRoles : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value.ToString() == "Administrator" || value.ToString() == "Banned" || value.ToString() == "NormalUser")
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Please enter a correct role('Banned', 'NormalUser' or 'Administrator')");
        }
    }
}
