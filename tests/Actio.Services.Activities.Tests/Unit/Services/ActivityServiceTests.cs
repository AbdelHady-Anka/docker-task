using System;
using System.Threading.Tasks;
using Actio.Domain.Entities;
using Actio.DomainServices.Repositories;
using Actio.DomainServices.Services;
using Moq;
using Xunit;

namespace Actio.Services.Activities.Tests.Unit.Services
{
    public class ActivityServiceTests
    {
        [Fact]
        public async Task add_async_should_be_succeed()
        {
            var categoryName = "test";
            var activityRepositoryMock = new Mock<IActivityRepository>();
            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            categoryRepositoryMock.Setup(x => x.GetByNameAsync(categoryName))
                .ReturnsAsync(new Category(categoryName));

            var activityService = new ActivityService(activityRepositoryMock.Object,
                categoryRepositoryMock.Object);

            var activityId = Guid.NewGuid();
            await activityService.AddAsync(activityId, Guid.NewGuid(), categoryName, "test", "test", DateTime.UtcNow);

            categoryRepositoryMock.Verify(x => x.GetByNameAsync(categoryName), Times.Once);
            activityRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Activity>()), Times.Once);

        }
    }
}