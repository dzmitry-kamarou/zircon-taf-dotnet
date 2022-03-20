using System;
using Microsoft.Extensions.Configuration;

namespace Commons
{
    public class BusinessConfig
    {
        private static readonly IConfigurationRoot Config = new ConfigurationBuilder()
            .AddJsonFile($"{Environment.GetEnvironmentVariable("ENV")}.model.json")
            .Build();

        public static readonly long RegisteredUserId = long.Parse(Config["registeredUserId"]);
        public static readonly string RegisteredUserEmail = Config["registeredUserEmail"];
        public static readonly string RegisteredUserPassword = CryptoUtil.Decrypt(Config["registeredUserPassword"]);
    }
}