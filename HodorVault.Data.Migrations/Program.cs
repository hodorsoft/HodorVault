using FluentMigrator.Runner;
using HodorVault.Data.Migrations.Migrations;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HodorVault.Data.Migrations
{
    class Program
    {
        /// <summary>
        /// Simple client to run sql migrations
        /// </summary>
        /// <param name="args">ConnectionString to database has to be the first and only argument</param>
        /// <example>dotnet run "Data Source=localhost;Initial Catalog=HodorVault;Integrated Security=true"</example>
        static void Main(string[] args)
        {
            if (args.Length == 0)
                throw new ArgumentException("Give connection string as the first argument");

            IServiceProvider serviceProvider = CreateServices(args[0]);

            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }

        /// <summary>
        /// Configure the dependency injection services
        /// </sumamry>
        private static IServiceProvider CreateServices(string connectionString)
        {
            return new ServiceCollection()
                // Add common FluentMigrator services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer2012()
                    // Set the connection string
                    .WithGlobalConnectionString(connectionString)
                    // Define the assembly containing the migrations
                    .WithMigrationsIn(typeof(CreateArtistsTable).Assembly))
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // Build the service provider
                .BuildServiceProvider(false);
        }

        /// <summary>
        /// Update the database
        /// </sumamry>
        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            // Execute the migrations
            runner.MigrateUp();
        }
    }
}
