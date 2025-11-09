using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace MottuBracelet.Tests.Integration
{
    public class MotoEndpointIntegrationTests :
        IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public MotoEndpointIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetMotos_DeveRetornar200()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/moto");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
