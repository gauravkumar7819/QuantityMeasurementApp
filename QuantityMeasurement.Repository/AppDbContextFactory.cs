using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.SqlServer;
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

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Use hardcoded connection string for design time
            var designTimeConnectionString = "Server=localhost\\SQLEXPRESS;Database=QuantityMeasurementDB;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=false";

            optionsBuilder.UseSqlServer(designTimeConnectionString, sqlServerOptions =>
            {
                sqlServerOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorNumbersToAdd: null);
            });

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}