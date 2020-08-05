using MemeSite.Domain.Validators;
using Moq;
using System.Threading.Tasks;
using Xunit;
using MemeSite.Domain.Interfaces;
using MemeSite.Domain.Models;
using MemeSite.Application.ViewModels;
using MemeSite.Application.Services;

namespace MemeSite.XUnitTests.Services
{
    public class FavouriteServiceTests
    {
        //readonly Mock<IGenericRepository<Favourite>> favouriteRepoMock;
        readonly Mock<IFavouriteRepository> favouriteRepoMock;

        public FavouriteServiceTests()
        {
            //favouriteRepoMock = new Mock<IGenericRepository<Favourite>>();
            favouriteRepoMock = new Mock<IFavouriteRepository>();
        }

        [Fact]
        public async Task AddFavourite()
        {
            var vm = new AddFavouriteVM()
            {
                MemeId = 1,
                UserId = "currentUserId",
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
