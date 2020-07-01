using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MemeSite.Domain.Validators
{
    public class FavouriteValidator : AbstractValidator<Favourite>
    {
        public FavouriteValidator()
        {
            RuleFor(m => m.MemeRefId)
                .NotEmpty()
                .WithMessage("Meme ID required");
            RuleFor(m => m.UserId)
                .NotEmpty()
                .WithMessage("User required");
        }
    }
}
