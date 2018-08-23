using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SolutionName.ApiTemplate;
using SolutionName.Infrastructure.Data;
using System;
using System.IO;
using System.Net.Http;
namespace SolutionName.IntegrationTest
{
    public abstract class BaseTest
    {
        private readonly TestServer server;
        private SqliteConnection connection;
        public HttpClient Client { get; set; }
        public ExampleContext Context { get; set; }

        public BaseTest()
        {
            var curDir = Directory.GetCurrentDirectory();

            var builder = new ConfigurationBuilder()
            .SetBasePath(curDir)
            .AddJsonFile("appsettings.json");

            server = new TestServer(new WebHostBuilder()
                .UseContentRoot(curDir)
                .UseConfiguration(builder.Build())
                .ConfigureServices(InitializeServices)
                .UseStartup<Startup>());
            Client = server.CreateClient();
        }

        protected virtual void InitializeServices(IServiceCollection services)
        {
            connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            services.AddDbContext<ExampleContext>(options =>
                options.UseSqlite(connection)
                .EnableSensitiveDataLogging());

            // Build the service provider.
            var sp = services.BuildServiceProvider();

            // Create a scope to obtain a reference to the database
            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                Context = scopedServices.GetRequiredService<ExampleContext>();

                try
                {
                    // Seed the database with test data.
                    DbInitializer.InitializeForTest(Context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}
