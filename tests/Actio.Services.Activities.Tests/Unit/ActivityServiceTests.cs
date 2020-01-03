using System;
using System.Threading.Tasks;
using Actio.Services.Activities.Domain.Models;
using Actio.Services.Activities.Domain.Repositories;
using Actio.Services.Activities.Services;
using Moq;
using Xunit;

namespace Actio.Services.Activities.Tests.Unit
{
    public class ActivityServiceTests
    {
        [Fact]
        public async Task activity_service_add_async_should_succeed()
        {
            var category = "test category";
            var activityRepositoryMock = new Mock<IActivityRepository>();
            var categoryRepositoryMock = new Mock<ICategoryRepository>();
            categoryRepositoryMock.Setup(x => x.GetAsync(category))
                .ReturnsAsync(new Category(category));
            var service = new ActivityService(activityRepositoryMock.Object,
                categoryRepositoryMock.Object);

            var id = Guid.NewGuid();
            var name = "test activity";
            var description = "test description";
            var userId = Guid.NewGuid();
            var createdAt = DateTime.UtcNow;

            await service.AddAsync(id, name, category,
                description, userId, createdAt);

            categoryRepositoryMock.Verify(x => x.GetAsync(It.IsAny<string>()), Times.Once);
            activityRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Activity>()), Times.Once);
        }
    }
}