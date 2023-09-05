﻿using Microsoft.Extensions.Configuration;
using System;

namespace IdentityService.Core.Configurations
{
    public static class Configuration
    {
        public static string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../IdentityService.Api"));
                configurationManager.AddJsonFile("appsettings.json");
                return configurationManager.GetConnectionString("DefaultConnection");
            }
        }

        public static string NpgsqlConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../K123ShopApp.WebApi"));
                configurationManager.AddJsonFile("appsettings.json");
                return configurationManager.GetConnectionString("NpgsqlConnection");
            }
        }

    }
}
