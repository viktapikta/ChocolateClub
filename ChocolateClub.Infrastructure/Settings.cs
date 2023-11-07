using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;
using System;

namespace ChocolateClub.Infrastructure
{
    public class Settings
    {
        static IConfiguration _config;

        public Settings()
        {
            
            _config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }
        public string DbConnectionString => GetAppSetting("DbConnectionString");

        private static string GetAppSetting(string key)
        {
            return _config.GetSection(key).Value ?? throw new Exception($"Missing appsetting: {key}");
        }
    }
}
