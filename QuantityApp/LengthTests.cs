
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurement.Business.Impl;
using QuantityMeasurement.Model.Units;
using QuantityMeasurement.Model.Models;
using QuantityMeasurement.Business.Interfaces;

using System;

namespace QuantityMeasurement.Tests
{
    [TestClass]
    public class LengthTests
    {
        private IQuantityMeasurementService service = null!;

        [TestInitialize]
        public void Setup()
        {
            service = new QuantityMeasurementServiceImpl();
        }

        [TestMethod]
        public void GivenTwoEqualFeetValues_ShouldReturnTrue()
        {
            bool result = service.AreLengthsEqual(5.0, LengthUnit.FEET, 5.0, LengthUnit.FEET);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenTwoDifferentFeetValues_ShouldReturnFalse()
        {
            bool result = service.AreLengthsEqual(5.0, LengthUnit.FEET, 6.0, LengthUnit.FEET);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenNegativeFeetValue_ShouldThrowException()
        {
            try
            {
                bool result = service.AreLengthsEqual(-5.0, LengthUnit.FEET, 6.0, LengthUnit.FEET);
                Assert.Fail("Expected ArgumentException was not thrown.");
            }
            catch (ArgumentException ex)
            {
                StringAssert.Contains(ex.Message, "FEET cannot be negative");
            }
        }
    }
}
