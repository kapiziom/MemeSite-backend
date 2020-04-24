using FluentValidation.Results;
using MemeSite.Data.Models;
using MemeSite.Data.Models.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MemeSite.XUnitTests.Validators
{
    public class MemeValidatorTests
    {
        MemeValidator memeValidator = new MemeValidator();

        [Fact]
        public void ValidMeme()
        {
            var meme = new Meme()
            {
                Title = "title",
                ByteHead = "data:image/jpeg;base64",
                ImageByte = GetByte(),
                CategoryId = 1,
                UserID = "correct_user_id"
            };

            ValidationResult result = memeValidator.Validate(meme);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void InvlidMeme()
        {
            var meme = new Meme()
            {
                Title = "",
                ByteHead = "data:image/jpeg;base64",
                ImageByte = GetByte(),
                CategoryId = 1,
                UserID = ""
            };

            ValidationResult result = memeValidator.Validate(meme);

            Assert.False(result.IsValid);
        }

        byte[] GetByte()
        {
            string input = "correct byte img";
            byte[] array = Encoding.ASCII.GetBytes(input);
            return array;
        }
    }
}
