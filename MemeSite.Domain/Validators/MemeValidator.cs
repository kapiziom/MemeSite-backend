﻿using FluentValidation;
using MemeSite.Domain.Models;

namespace MemeSite.Domain.Validators
{
    public class MemeValidator : AbstractValidator<Meme>
    {
        public MemeValidator()
        {
            RuleFor(m => m.Title)
                .NotEmpty()
                .WithMessage("Title required");
            RuleFor(m => m.ByteHead)
                .NotEmpty()
                .WithMessage("ByteHead required like this 'data:image/jpeg;base64'");
            RuleFor(m => m.ImageByte)
                .NotEmpty()
                .WithMessage("Image required");
            RuleFor(m => m.CategoryId)
                .NotEmpty()
                .WithMessage("Category required");
            RuleFor(m => m.UserID)
                .NotEmpty()
                .WithMessage("User id required");

        }
    }
}
