using MemeSite.Data.DbContext;
using MemeSite.Data.Repository;
using MemeSite.Api.Services;
using MemeSite.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using MemeSite.Domain;
using MemeSite.Domain.Enums;
using MemeSite.Domain.Validators;

namespace MemeSite.XUnitTests.Services
{

    public class VoteServiceTests
    {
        readonly Mock<IGenericRepository<Vote>> voteRepoMock;


        public VoteServiceTests()
        {
            voteRepoMock = new Mock<IGenericRepository<Vote>>();
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
