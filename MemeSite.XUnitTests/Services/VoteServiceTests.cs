using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using MemeSite.Domain.Validators;
using MemeSite.Domain.Models;
using MemeSite.Domain.Interfaces;
using MemeSite.Application.Services;
using MemeSite.Application.ViewModels;

namespace MemeSite.XUnitTests.Services
{

    public class VoteServiceTests
    {

        //readonly Mock<IGenericRepository<Vote>> voteRepoMock;
        readonly Mock<IVoteRepository> voteRepoMock;

        public VoteServiceTests()
        {
            //voteRepoMock = new Mock<IGenericRepository<Vote>>();
            voteRepoMock = new Mock<IVoteRepository>();
            voteRepoMock.Setup(x => x.InsertAsync(It.IsAny<Vote>())).ReturnsAsync((Vote x) => x);
        }


        [Fact]
        public void InsertSecondVoteOnSameItem()
        {
            string userId = "CurrentUserID";
            var sendVote = new SendVoteVM()
            {
                MemeRefId = 1,
                Value = Value.downvote
            };

            var service = new VoteService(voteRepoMock.Object, new VoteValidator());
            voteRepoMock.Setup(x => x.IsExistAsync(y => y.MemeRefId == sendVote.MemeRefId && y.UserId == userId)).ReturnsAsync(true);
            var result = service.InsertVote(sendVote, userId);
            Assert.Equal("U have voted for this", result.Exception.InnerException.Message);

        }

        [Fact]
        public void InsertNewVote()
        {
            string userId = "CurrentUserID";
            var sendVote = new SendVoteVM()
            {
                MemeRefId = 1,
                Value = Value.downvote
            };

            var service = new VoteService(voteRepoMock.Object, new VoteValidator());
            voteRepoMock.Setup(x => x.IsExistAsync(y => y.MemeRefId == sendVote.MemeRefId && y.UserId == userId)).ReturnsAsync(false);
            var result = service.InsertVote(sendVote, userId);
            Assert.NotNull(result);
            Assert.True(result.Result.Succeeded);
        }

        [Fact]
        public void CorrectUpdateVote()
        {
            string userId = "CurrentUserID";
            var sendVote = new SendVoteVM()
            {
                MemeRefId = 1,
                Value = Value.downvote
            };

            var service = new VoteService(voteRepoMock.Object, new VoteValidator());
            voteRepoMock.Setup(x => x.FindAsync(y => y.MemeRefId == sendVote.MemeRefId && y.UserId == userId))
                .ReturnsAsync(new Vote() { MemeRefId = 1, UserId = userId, Value = Value.upvote});
            var result = service.UpdateVote(sendVote, userId);
            Assert.NotNull(result);
            Assert.True(result.Result.Succeeded);
        }

        [Fact]
        public void UpdateVoteNotFound()
        {
            string userId = "CurrentUserID";
            var sendVote = new SendVoteVM()
            {
                MemeRefId = 1,
                Value = Value.downvote
            };

            var service = new VoteService(voteRepoMock.Object, new VoteValidator());
            var result = service.UpdateVote(sendVote, userId);
            Assert.Equal("Not Found", result.Exception.InnerException.Message);
        }

    }
}
