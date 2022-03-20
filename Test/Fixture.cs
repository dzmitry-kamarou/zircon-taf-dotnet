using System;
using System.Net;
using System.Net.Http;
using Api.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;

namespace Test
{
    public class Fixture
    {
        public void ConfigureServices(ServiceCollection serviceCollection)
        {
            serviceCollection
                .AddLogging(SetupLogging)
                .AddHttpClient<UserApiService>(SetupDefaultRequestHeaders)
                .AddPolicyHandler(GetRetryPolicy());
        }

        private static void SetupLogging(ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.AddConsole();
            loggingBuilder.SetMinimumLevel(LogLevel.Debug);
        }

        private static void SetupDefaultRequestHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("Connection", "close");
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
                .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(Math.Pow(0.4, attempt)));
        }
    }
}