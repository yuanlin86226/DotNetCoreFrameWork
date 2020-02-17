using System;
using Context;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Seed;

namespace api
{
#pragma warning disable CS1591
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<CustomContext>();
                try
                {
                    DataSeeder.Initialize(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            host.Run();

            // CreateWebHostBuilder(args).Build().Run();
        }
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) => //呼叫IIS 設定Web Server
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((webHostBuilder, configurationBinder) =>
                {
                    configurationBinder.AddJsonFile("appsettings.json", optional: true);
                })
                .UseStartup<Startup>();

    }
#pragma warning restore CS1591
}

