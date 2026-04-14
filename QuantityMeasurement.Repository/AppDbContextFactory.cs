using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using System.IO;

namespace QuantityMeasurement.Repository
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "QuantityMeasurement.API"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            // Replace environment variable placeholder if needed
            if (connectionString?.StartsWith("${") == true)
            {
                var envVar = connectionString.Substring(2, connectionString.Length - 3);
                connectionString = Environment.GetEnvironmentVariable(envVar);
            }

            if (string.IsNullOrEmpty(connectionString))
            {
                // Use hardcoded connection string for design time
                var designTimeConnectionString = "Server=dpg-d7adklh5pdvs73dd9i60-a.oregon-postgres.render.com;Port=5432;Database=quantitymeasurementdb_zueb;User Id=quantitymeasurementdb_zueb_user;Password=dfpSD1O3C4uBQTEN0hq3ZLJtuQ94NhtT;SslMode=Require;TrustServerCertificate=true;";
                connectionString = designTimeConnectionString;
            }

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorCodesToAdd: null);
            });

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}