using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurement.Core;
using QuantityMeasurement.Core.Models;
using System;

namespace QuantityMeasurement.Tests
{
    [TestClass]
    public class FeetTests
    {
        [TestMethod]
        public void GivenTwoEqualFeetValues_ShouldReturnTrue()
        {
            bool result = QuantityMeasurementApp.AreFeetEqual(5.0, 5.0);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenTwoDifferentFeetValues_ShouldReturnFalse()
        {
            bool result = QuantityMeasurementApp.AreFeetEqual(5.0, 6.0);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenNegativeFeetValue_ShouldThrowException()
        {
            try
            {
                bool result = QuantityMeasurementApp.AreFeetEqual(-5.0, 6.0);
                // If no exception is thrown, fail the test
                Assert.Fail("Expected ArgumentException was not thrown.");
            }
            catch (ArgumentException ex)
            {
                // Test passes if ArgumentException is thrown
                StringAssert.Contains(ex.Message, "Feet cannot be negative");
            }
        }
    }
}