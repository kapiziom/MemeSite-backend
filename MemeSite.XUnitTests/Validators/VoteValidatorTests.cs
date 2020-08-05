using FluentValidation.Results;
using MemeSite.Domain.Models;
using MemeSite.Domain.Validators;
using Xunit;

namespace MemeSite.XUnitTests.Validators
{
    public class VoteValidatorTests
    {

        VoteValidator voteValidator = new VoteValidator();

        [Fact]
        public void ValidVote()
        {
            var vote = new Vote()
            {
                MemeRefId = 1,
                Value = Value.upvote,
                UserId = "correct_user_id"
            };

            ValidationResult result = voteValidator.Validate(vote);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void InvalidVote0()
        {
            var vote = new Vote()
            {
                MemeRefId = 1,
                UserId = "correct_user_id"
            };

            ValidationResult result = voteValidator.Validate(vote);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void InvalidVote1()
        {
            var vote = new Vote()
            {
                MemeRefId = 1,
                Value = Value.downvote,
            };

            ValidationResult result = voteValidator.Validate(vote);

            Assert.False(result.IsValid);
        }
    }
}
