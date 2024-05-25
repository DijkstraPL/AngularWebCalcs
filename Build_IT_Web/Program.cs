using Build_IT_WebInfrastructure.Identity;
using Build_IT_WebInfrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Build_IT_WebTests")]
namespace Build_IT_Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Initialise and seed database
            using (var scope = host.Services.CreateScope())
            { 
                var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
                await initialiser.InitialiseAsync();
                await initialiser.SeedAsync();
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}