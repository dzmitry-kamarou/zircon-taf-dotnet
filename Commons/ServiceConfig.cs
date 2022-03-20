using System;
using Microsoft.Extensions.Configuration;

namespace Commons
{
    public class ServiceConfig
    {
        private static readonly IConfigurationRoot Config = new ConfigurationBuilder()
            .AddJsonFile($"{Environment.GetEnvironmentVariable("ENV")}.json")
            .Build();

        public static readonly string BaseApiUri = Config["baseApiUri"];
    }
}