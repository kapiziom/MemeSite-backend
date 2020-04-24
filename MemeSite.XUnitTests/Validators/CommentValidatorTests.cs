using FluentValidation.Results;
using MemeSite.Data.Models;
using MemeSite.Data.Models.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MemeSite.XUnitTests.Validators
{
    public class CommentValidatorTests
    {
        CommentValidator commentValidator = new CommentValidator();

        [Fact]
        public void ValidComment()
        {
            var comment = new Comment()
            {
                Txt = "asdassadda",
                UserID = "correct_user_id",
                MemeRefId = 1
            };

            ValidationResult result = commentValidator.Validate(comment);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void InvalidComment()
        {
            var comment = new Comment()
            {
                Txt = "ee",
                UserID = "correct_user_id",
                MemeRefId = 1
            };

            ValidationResult result = commentValidator.Validate(comment);

            Assert.False(result.IsValid);
        }
    }
}
