using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.ViewModels
{
    public class SendVoteVM
    {
        [Required]
        [PossibleValues]
        public int Value { get; set; }//-1 or 1

        [Required]
        public int MemeRefId { get; set; }
    }

    public class PossibleValues : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value.ToString() == "1" || value.ToString() == "-1")
            {
                return ValidationResult.Success;
            }


            return new ValidationResult("Please enter a correct value");
        }
    }
}
