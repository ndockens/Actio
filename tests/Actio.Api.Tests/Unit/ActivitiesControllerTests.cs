using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using RawRabbit;
using Actio.Api.Controllers;
using Actio.Api.Repositories;
using Actio.Common.Commands;

namespace Actio.Api.Tests.Unit
{
    public class ActivitiesControllerTests
    {
        [Fact]
        public async Task activities_controller_post_should_return_correct_url()
        {
            var busClientMock = new Mock<IBusClient>();
            var activityRepositoryMock = new Mock<IActivityRepository>();
            var controller = new ActivitiesController(busClientMock.Object,
                activityRepositoryMock.Object);
            var userId = Guid.NewGuid();

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(
                        new ClaimsIdentity(
                            new Claim[] { new Claim(ClaimTypes.Name, userId.ToString()) })
                        )
                }
            };

            var command = new CreateActivity
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Name = "Test Activity"
            };

            var result = await controller.Post(command);

            var contentResult = result as AcceptedResult;

            contentResult.Location.Should().BeEquivalentTo($"activities/{command.Id}");
        }
    }
}