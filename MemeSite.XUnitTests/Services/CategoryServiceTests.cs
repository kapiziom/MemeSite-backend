using MemeSite.Data.Models;
using MemeSite.Data.Models.Validators;
using MemeSite.Data.Repository;
using MemeSite.Services;
using MemeSite.ViewModels;
using Moq;
using Xunit;

namespace MemeSite.XUnitTests.Services
{

    public class CategoryServiceTests
    {
        readonly Mock<IGenericRepository<Category>> categoryRepoMock;

        public CategoryServiceTests()
        {

            categoryRepoMock = new Mock<IGenericRepository<Category>>();

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
