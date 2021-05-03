using BelgianCartoons;
using BelgianCartoons.Abstract.Services;
using BelgianCartoons.Core.Services;
using BelgianCartoons.Core.Settings;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

[assembly: FunctionsStartup(typeof(BelgianCartoons.Scraper.Functions.Startup))]

namespace BelgianCartoons.Scraper.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // Note: Only register dependencies, do not depend or request those in Configure().
            // Dependencies are only usable during function execution, not before (like here).

            builder.Services.AddHttpClient();

            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", true, true)
                .AddEnvironmentVariables();

            var config = configurationBuilder.Build();

            builder.Services.Configure<RedditSettings>(config.GetSection("Reddit"));
            builder.Services.AddTransient((serviceProvider) => serviceProvider.GetService<IOptions<RedditSettings>>().Value);

            builder.Services.Configure<TwitterSettings>(config.GetSection("Twitter"));
            builder.Services.AddTransient((serviceProvider) => serviceProvider.GetService<IOptions<TwitterSettings>>().Value);

            builder.Services.AddScoped<ITwitterService, TwitterService>();
            builder.Services.AddScoped<IRedditService, RedditService>();
        }
    }
}