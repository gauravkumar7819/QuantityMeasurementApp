// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using QuantityMeasurement.Repository.Interfaces;
// using QuantityMeasurement.Repository.Implementations;
// using QuantityMeasurement.Model.Entities;
// using System.Linq;

// namespace QuantityMeasurement.Tests
// {
//     [TestClass]
//     public class RepositoryTests
//     {
//         private IQuantityMeasurementRepository _adoRepository = null!;

//         [TestInitialize]
//         public void Setup()
//         {
//             // Using the actual ADO repository to test database interaction
//             _adoRepository = new QuantityMeasurementADORepository();
//             // Clean up the database before each test to ensure isolation
//             _adoRepository.DeleteAll();
//         }

//         [TestCleanup]
//         public void Cleanup()
//         {
//             _adoRepository.DeleteAll();
//         }

//         [TestMethod]
//         public void SaveEntity_ShouldPersistToDatabase()
//         {
//             // Arrange
//             var entity = new QuantityMeasurementEntity("ADD", "1 FEET", "2 FEET", "3 FEET");

//             // Act
//             _adoRepository.Save(entity);
//             var allMeasurements = _adoRepository.GetAll();

//             // Assert
//             Assert.HasCount(1, allMeasurements);
//             Assert.AreEqual("ADD", allMeasurements.First().Operation);
//             Assert.AreEqual("3 FEET", allMeasurements.First().Result);
//         }

//         [TestMethod]
//         public void RetrieveAllMeasurements_ShouldReturnAllSavedEntities()
//         {
//             // Arrange
//             _adoRepository.Save(new QuantityMeasurementEntity("ADD", "1 INCH", "2 INCH", "3 INCH"));
//             _adoRepository.Save(new QuantityMeasurementEntity("SUBTRACT", "5 FEET", "2 FEET", "3 FEET"));

//             // Act
//             var allMeasurements = _adoRepository.GetAll();

//             // Assert
//             Assert.HasCount(2, allMeasurements);
//         }

//         [TestMethod]
//         public void Save_ShouldPreventSqlInjection()
//         {
//             // Arrange
//             string maliciousPayload = "' OR 1=1; --";
//             var entity = new QuantityMeasurementEntity("COMPARE", maliciousPayload, "1 FEET", "Error");

//             // Act
//             _adoRepository.Save(entity);
//             var allMeasurements = _adoRepository.GetAll();

//             // Assert
//             Assert.HasCount(1, allMeasurements);
//             Assert.AreEqual(maliciousPayload, allMeasurements.First().Operand1);
//         }

//         [TestMethod]
//         public void DateTimeHandling_ShouldPreserveTimestamp()
//         {
//             // Arrange
//             var entity = new QuantityMeasurementEntity("CONVERT", "100 C", "-", "212 F");
//             var timeBeforeSave = System.DateTime.Now;

//             // Act
//             _adoRepository.Save(entity);
//             var savedEntity = _adoRepository.GetAll().First();

//             // Assert
//             // Check if the saved timestamp is recent (e.g., within a 5-second window)
//             Assert.IsLessThan(5.0, (savedEntity.TimeStamp - timeBeforeSave).TotalSeconds);
//         }
//     }
// }
