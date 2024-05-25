using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Build_IT_WebApplication.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Build_IT_WebInfrastructure.Services
{
    public class AppConfigurationProvider : IAppConfigurationProvider
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;
        private readonly SecretClient _client;

        private string _connectionString = null;
        private string _redisUrl = null;

        public AppConfigurationProvider(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            var keyVaultName = _configuration.GetSection("Azure").GetValue<string>("KeyVaultName");
            if (!string.IsNullOrWhiteSpace(keyVaultName))
            {
                var options = new SecretClientOptions()
                {
                    Retry =
                    {
                        Delay= TimeSpan.FromSeconds(2),
                        MaxDelay = TimeSpan.FromSeconds(16),
                        MaxRetries = 5,
                        Mode = RetryMode.Exponential
                    }
                };
                _client = new SecretClient(new Uri($"https://{keyVaultName}.vault.azure.net/"), new DefaultAzureCredential(), options);
            }
        }

        public string GetConnectionString()
        {
            if (_environment.IsProduction())
                return _connectionString ??= GetAzureSecret("DbConnectionString");
            return _connectionString ??= _configuration.GetConnectionString("DefaultConnection");
        }
        public string GetRedisUrl()
        {
            if (_environment.IsProduction())
                return _redisUrl ??= GetAzureSecret("RedisUrl");
            return "";
        }


        private string GetAzureSecret(string secretName)
        {
            KeyVaultSecret secret = _client.GetSecret(secretName);
            return secret.Value;
        }
    }
}
