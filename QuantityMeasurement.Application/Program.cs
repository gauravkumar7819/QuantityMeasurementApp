using QuantityMeasurement.Business.Interfaces;
using QuantityMeasurement.Business.Impl;
using QuantityMeasurement.Repository.Implementations;
using QuantityMeasurement.Repository.Interfaces;
using QuantityMeasurement.Repository;
using Microsoft.EntityFrameworkCore;


namespace QuantityMeasurement.Application
{
    class Program
    {
        static void Main()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Database=quantitymeasurementdb;Username=postgres;Password=password");
            var context = new AppDbContext(optionsBuilder.Options);

            IQuantityMeasurementRepository repo = new QuantityMeasurementRepository(context);
            IQuantityMeasurementService service = new QuantityMeasurementServiceImpl(repo);

            IMenu menu = new Menu();
            menu.Start(service);
        }
    }
}