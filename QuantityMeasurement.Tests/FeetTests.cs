using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurement.Core.Models;

namespace QuantityMeasurement.Tests
{
    [TestClass]
    public class FeetTests
    {
        [TestMethod]
        public void GivenTwoEqualFeetValues_ShouldReturnTrue()
        {
            var firstFeet = new Feet(5.0);
            var secondFeet = new Feet(5.0);
            bool result = firstFeet.Equals(secondFeet);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenTwoDifferentFeetValues_ShouldReturnFalse()
        {
            var firstFeet = new Feet(5.0);
            var secondFeet = new Feet(6.0);

            bool result = firstFeet.Equals(secondFeet);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenSameReference_ShouldReturnTrue()
        {
            var firstFeet = new Feet(5.0);

            bool result = firstFeet.Equals(firstFeet);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenNullObject_ShouldReturnFalse()
        {
            var firstFeet = new Feet(5.0);

            bool result = firstFeet.Equals(null);

            Assert.IsFalse(result);
        }
    }
}