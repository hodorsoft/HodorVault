using Dapper;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlClient;

namespace HodorVault.Data.Tests
{
    public static class TestDbHelper
    {
        public static string ConnectionString = "data source=localhost;initial catalog=hodorvault.integrationtests;integrated security=true";

        /// <summary>
        /// SQLite in-memory db for testing purposes.
        /// Migrations are ran from HodorVault.Data.Migrations.Migrations
        /// </summary>
        /// <param name="connection"></param>
        public static void Initialize()
        {
            var serviceProvider = new ServiceCollection()
                // Add common FluentMigrator services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer2012()
                    // Set the connection string
                    .WithGlobalConnectionString(ConnectionString)
                    // Define the assembly containing the migrations
                    .WithMigrationsIn(typeof(HodorVault.Data.Migrations.Migrations.CreateArtistsTable).Assembly))
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // Build the service provider
                .BuildServiceProvider(false);

            using (var scope = serviceProvider.CreateScope())
            {
                // Instantiate the runner
                var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

                // Execute the migrations
                runner.MigrateUp();
            }
        }

        public static void TearDown()
        {
            using(var c = new SqlConnection(ConnectionString))
            {
                c.Execute("delete from albums");
                c.Execute("delete from artists");
            }
        }
    }
}
