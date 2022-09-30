using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AdaBank.IntegrationTests
{
    public class CustomFactory : IClassFixture<WebApplicationFactory<Api.Program>>
    {
        public CustomFactory(WebApplicationFactory<Api.Program> factory)
        {
            var client = factory.WithWebHostBuilder(builder =>
            {

                builder.ConfigureTestServices(services => 
                {
                    //services.RemoveAll<IHostedService>();
                    //services.AddTransient<IHostedService, HostedService>();
                });
            })
            .CreateClient();
        }
    }
}
