using MemeSite.Data.Models;
using MemeSite.Data.Models.Validators;
using MemeSite.Data.Repository;
using MemeSite.Services;
using MemeSite.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MemeSite.XUnitTests.Services
{
    public class FavouriteServiceTests
    {
        readonly Mock<IGenericRepository<Favourite>> favouriteRepoMock;

        public FavouriteServiceTests()
        {
            favouriteRepoMock = new Mock<IGenericRepository<Favourite>>();
        }

        [Fact]
        public async Task AddFavourite()
        {
            var vm = new AddFavouriteVM()
            {
                MemeId = 1,
                UserId = "currentUserId"
            };
            favouriteRepoMock.Setup(x =>
            x.IsExistAsync(m => m.MemeRefId == vm.MemeId && m.UserId == vm.UserId))
                .ReturnsAsync(false);
            var service = new FavouriteService(favouriteRepoMock.Object, new FavouriteValidator());
            bool result = await service.InsertFavourite(vm);
            Assert.True(result);
        }

        [Fact]
        public async Task AddSameFavourite()
        {
            var vm = new AddFavouriteVM()
            {
                MemeId = 1,
                UserId = "currentUserId"
            };
            favouriteRepoMock.Setup(x =>
            x.IsExistAsync(m => m.MemeRefId == vm.MemeId && m.UserId == vm.UserId))
                .ReturnsAsync(true);

            var service = new FavouriteService(favouriteRepoMock.Object, new FavouriteValidator());
            bool result = await service.InsertFavourite(vm);
            Assert.False(result);
        }

    }
}
