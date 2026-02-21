using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurement.Core;
using QuantityMeasurement.Core.Models;
using System;

namespace QuantityMeasurement.Tests
{
    [TestClass]
    public class InchesTests
    {
        [TestMethod]
        public void GivenTwoEqualInchesValues_ShouldReturnTrue()
        {
            bool result = QuantityMeasurementApp.AreInchesEqual(12.0, 12.0);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenTwoDifferentInchesValues_ShouldReturnFalse()
        {
            bool result = QuantityMeasurementApp.AreInchesEqual(12.0, 10.0);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenNegativeInchesValue_ShouldThrowException()
        {
            try
            {
                bool result = QuantityMeasurementApp.AreInchesEqual(-5.0, 6.0);
                // Agar exception nahi aayi, test fail karo
                Assert.Fail("Expected ArgumentException was not thrown.");
            }
            catch (ArgumentException ex)
            {
                // Agar ArgumentException aayi to test pass
                StringAssert.Contains(ex.Message, "Inches cannot be negative");
            }
        }
    }
}