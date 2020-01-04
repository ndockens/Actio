using Xunit;
using FluentAssertions;
using Actio.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;

namespace Actio.Api.Tests.Integration
{
    public class HomeControllerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public HomeControllerTests()
        {
            _server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task home_controller_get_should_return_string_content()
        {
            var response = await _client.GetAsync("/");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            content.Should().BeEquivalentTo("Hello world");
        }
    }
}