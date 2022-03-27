using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Test.Api
{
    public class BaseApiTest : IClassFixture<WebApplicationFactory<Fixture>>
    {
        protected readonly ServiceProvider ServiceProvider;

        protected BaseApiTest()
        {
            var collection = new ServiceCollection();
            new Fixture().ConfigureServices(collection);
            ServiceProvider = collection.BuildServiceProvider();
        }
    }
}