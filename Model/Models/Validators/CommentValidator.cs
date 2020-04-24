using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MemeSite.Data.Models.Validators
{
    public class CommentValidator : AbstractValidator<Comment>
    {
        public CommentValidator()
        {
            RuleFor(m => m.Txt)
                .NotEmpty()
                .WithMessage("text required");
            RuleFor(m => m.Txt)
                .MaximumLength(2000)
                .WithMessage("Maximum length of txt is 2000");
            RuleFor(m => m.Txt)
                .MinimumLength(3)
                .WithMessage("Minimum length of txt is 3");
            RuleFor(m => m.UserID)
                .NotEmpty()
                .WithMessage("User required");
            RuleFor(m => m.MemeRefId)
                .NotEmpty()
                .WithMessage("Meme ID required");
        }
    }
}
