using FluentValidation;
using MemeSite.Domain.Enums;
using System.Collections.Generic;
using System.Text;


namespace MemeSite.Domain.Validators
{
    public class VoteValidator : AbstractValidator<Vote>
    {
        public VoteValidator()
        {
            RuleFor(m => m.Value)
                .NotEmpty()
                .WithMessage("Value required")
                .Must(ValueIsValid)
                .WithMessage("Value must be equal '1' or '-1'");
            RuleFor(m => m.MemeRefId)
                .NotEmpty()
                .WithMessage("Meme ID required");
            RuleFor(m => m.UserId)
                .NotEmpty()
                .WithMessage("User required");
        }

        private bool ValueIsValid(Value value)
        {
            if (value == (Value)(1) || value == (Value)(-1))
                return true;
            return false;
        }

    }
}
