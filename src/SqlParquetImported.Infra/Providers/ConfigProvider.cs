using Microsoft.Extensions.Configuration;
using SqlParquetImporter.Domain.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqlParquetImported.Infra.Providers
{
    public class ConfigProvider : IConfigProvider
    {
        private readonly IConfiguration _config;
        public ConfigProvider(IConfiguration configuration)
        {
            _config = configuration;

        }

        public string BaseFilePath => _config[GetKey("BaseFilePath")];

        
        private string GetKey(string key, string @namespace = "Custom")
        {
            return $"{@namespace}:{key}";
        }
    }
}
