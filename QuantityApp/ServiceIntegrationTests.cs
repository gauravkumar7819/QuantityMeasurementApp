using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurement.Business.Interfaces;
using QuantityMeasurement.Business.Impl;
using QuantityMeasurement.Repository.Interfaces;
using QuantityMeasurement.Repository.Implementations;
using QuantityMeasurement.Repository;
using Microsoft.EntityFrameworkCore;
using QuantityMeasurement.Model.Units;
using System.Linq;

namespace QuantityMeasurement.Tests
{
    [TestClass]
    public class ServiceIntegrationTests
    {
        [TestMethod]
        public void ServiceWithDatabaseRepository_ShouldPersistData()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            var context = new AppDbContext(options);
            IQuantityMeasurementRepository dbRepo = new QuantityMeasurementRepository(context);
            dbRepo.DeleteAll(); // Clean slate
            IQuantityMeasurementService service = new QuantityMeasurementServiceImpl(dbRepo);

            // Act
            service.AreLengthsEqual(1, LengthUnit.FEET, 1, LengthUnit.FEET);

            // Assert
            Assert.HasCount(1, dbRepo.GetAll());
            
            // Cleanup
            dbRepo.DeleteAll();
        }

        [TestMethod]
        public void ServiceWithCacheRepository_ShouldStoreDataInMemory()
        {
            // Arrange
            IQuantityMeasurementRepository cacheRepo = new QuantityMeasurementCacheRepository();
            IQuantityMeasurementService service = new QuantityMeasurementServiceImpl(cacheRepo);

            // Act
            service.AreTemperaturesEqual(100, TemperatureUnit.CELSIUS, 212, TemperatureUnit.FAHRENHEIT);

            // Assert
            Assert.HasCount(1, cacheRepo.GetAll());
            Assert.AreEqual("COMPARE_TEMPERATURE", cacheRepo.GetAll().First().Operation);
        }
    }
}
