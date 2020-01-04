using Xunit;
using FluentAssertions;
using Actio.Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Actio.Api.Tests.Unit
{
    public class HomeControllerTests
    {
        [Fact]
        public void home_controller_get_should_return_string_value()
        {
            var controller = new HomeController();

            var result = controller.Get() as ContentResult;

            result.Content.Should().BeEquivalentTo("Hello world");
        }
    }
}