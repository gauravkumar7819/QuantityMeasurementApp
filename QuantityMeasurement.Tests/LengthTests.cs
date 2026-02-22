using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurement.Core;
using QuantityMeasurement.Core.Models;
using System;

namespace QuantityMeasurement.Tests
{
    [TestClass]
    public class LengthTests
    {
        [TestMethod]
        public void GivenEqualFeetAndInches_ShouldReturnTrue()
        {
            bool result = QuantityMeasurementApp.AreLengthsEqual(1.0, LengthUnit.FEET, 12.0, LengthUnit.INCHES);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenDifferentFeetAndInches_ShouldReturnFalse()
        {
            bool result = QuantityMeasurementApp.AreLengthsEqual(1.0, LengthUnit.FEET, 11.0, LengthUnit.INCHES);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenNegativeLength_ShouldThrowException()
        {
            try
            {
                bool result = QuantityMeasurementApp.AreLengthsEqual(-1.0, LengthUnit.FEET, 12.0, LengthUnit.INCHES);
                Assert.Fail("Expected ArgumentException was not thrown.");
            }
            catch (ArgumentException ex)
            {
                StringAssert.Contains(ex.Message, "FEET cannot be negative");
            }
        }

        [TestMethod]
        public void GivenNegativeInches_ShouldThrowException()
        {
            try
            {
                bool result = QuantityMeasurementApp.AreLengthsEqual(1.0, LengthUnit.FEET, -12.0, LengthUnit.INCHES);
                Assert.Fail("Expected ArgumentException was not thrown.");
            }
            catch (ArgumentException ex)
            {
                StringAssert.Contains(ex.Message, "INCHES cannot be negative");
            }
        }
    }
}