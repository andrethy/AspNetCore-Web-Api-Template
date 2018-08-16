using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using SolutionName.Infrastructure.Data;

namespace SolutionName.ApiTemplate
{
    public class Program
    {
        public static IConfiguration config { get; set; }

        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            config = builder.Build();

            SetupLogger(args);
            SetupSeeding(host, args);

            host.Run();
        }

        private static void SetupLogger(string[] args)
        {
            //Default connectionString
            var connectionString = "Data Source=YOUR_LIVE_SERVER_IP;Initial Catalog=YOUR_LIVE_DB_NAME;persist security info=True;user id=USERNAME;password=PASSWORD";

            //Connectionstring for development
            if (GetHostingEnvironment(args).IsDevelopment())
                connectionString = "Server=(local);Database=ExampleDB;Trusted_Connection=True;MultipleActiveResultSets=true";

            var tableName = "Logs";

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Warning()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                .WriteTo.MSSqlServer(connectionString, tableName, autoCreateSqlTable: true)
                .CreateLogger();
        }

        private static void SetupSeeding(IWebHost host, string[] args)
        {
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    //Gets the context and calls the appropiate seeding method
                    var context = serviceProvider.GetRequiredService<ExampleContext>();
                    if (GetHostingEnvironment(args).IsDevelopment())
                    {
                        DbInitializer.InitializeForDevelopment(context);
                    }
                }
                catch (Exception ex)
                {
                    //Use a logger to log the exception.
                    Log.Error(ex, "An error occurred while initializing / seeding the database.");
                }
            }
        }

        private static IHostingEnvironment GetHostingEnvironment(string[] args)
        {
            using (var scope = CreateWebHostBuilder(args).Build().Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                return services.GetService<IHostingEnvironment>();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog();
    }
}
