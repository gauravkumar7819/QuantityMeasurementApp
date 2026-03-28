using Microsoft.EntityFrameworkCore;
using QuantityMeasurement.Model.Entities;

namespace QuantityMeasurement.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<QuantityMeasurementEntity> QuantityMeasurements { get; set; }
    }
}