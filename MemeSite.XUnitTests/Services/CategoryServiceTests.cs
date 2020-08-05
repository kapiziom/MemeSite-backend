using MemeSite.Domain.Validators;
using Moq;
using Xunit;
using MemeSite.Domain.Interfaces;
using MemeSite.Domain.Models;
using MemeSite.Application.ViewModels;
using MemeSite.Application.Services;

namespace MemeSite.XUnitTests.Services
{

    public class CategoryServiceTests
    {
        //readonly Mock<IGenericRepository<Category>> categoryRepoMock;
        readonly Mock<ICategoryRepository> categoryRepoMock;

        public CategoryServiceTests()
        {

            //categoryRepoMock = new Mock<IGenericRepository<Category>>();
            categoryRepoMock = new Mock<ICategoryRepository>();

            categoryRepoMock.Setup(x => x.InsertAsync(It.IsAny<Category>())).ReturnsAsync((Category x) => x);

        }

        [Fact]
        public void InsertCategoryDuplicate()
        {
            var create = new CreateCategoryVM()
            {
                CategoryName = "name",
            };

            var service = new CategoryService(categoryRepoMock.Object, new CategoryValidator());
            categoryRepoMock.Setup(x => x.IsExistAsync(y => y.CategoryName == create.CategoryName)).ReturnsAsync(true);
            var result = service.InsertCategory(create);
            Assert.Equal("Duplicate, category already exist.", result.Exception.InnerException.Message);
        
        }

        [Fact]
        public void InsertCategoryNotDuplicate()
        {
            var create = new CreateCategoryVM()
            {
                CategoryName = "name",
            };

            var service = new CategoryService(categoryRepoMock.Object, new CategoryValidator());
            categoryRepoMock.Setup(x => x.IsExistAsync(y => y.CategoryName == create.CategoryName)).ReturnsAsync(false);
            var result = service.InsertCategory(create);

            Assert.NotNull(result);
            Assert.True(result.Result.Succeeded);
            Assert.Equal("name", result.Result.Value.CategoryName);

        }
    }
}
