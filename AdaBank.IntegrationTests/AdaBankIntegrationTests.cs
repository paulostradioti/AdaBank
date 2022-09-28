using AdaBank.IntegrationTests.Models;
using AdaBank.IntegrationTests.TestHelpers.Serialization;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;

namespace AdaBank.IntegrationTests
{
    public class AdaBankIntegrationTests : IClassFixture<WebApplicationFactory<Api.Program>>
    {
        private readonly WebApplicationFactory<Api.Program> factory;
        private readonly HttpClient client;

        public AdaBankIntegrationTests(WebApplicationFactory<Api.Program> factory)
        {
            this.factory = factory;
            this.client = factory.CreateDefaultClient();

            // 
            /*
            var clientOptions = new WebApplicationFactoryClientOptions();
            clientOptions.AllowAutoRedirect = true;
            clientOptions.BaseAddress = new Uri("http://localhost");
            clientOptions.HandleCookies = true;
            clientOptions.MaxAutomaticRedirections = 7;
            _client = _factory.CreateClient(clientOptions);
            */
        }

        [Fact]
        public async Task HealthCheck_ReturnsOk()
        {
            //var application = new WebApplicationFactory<Api.Program>().WithWebHostBuilder(builder => { });
            var response = await client.GetAsync("/healthcheck");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Get_ReturnsCollection()
        {
            //var application = new WebApplicationFactory<Api.Program>().WithWebHostBuilder(builder => { });
            var response = await client.GetStreamAsync("/WeatherForecast");
            var model = JsonSerializer.Deserialize<List<WeatherForecastGetRequestModel>>(response, SerializationOptions.DefaultDeserializationOptions);

            Assert.Equal(5, model?.Count);
        }
    }
}
