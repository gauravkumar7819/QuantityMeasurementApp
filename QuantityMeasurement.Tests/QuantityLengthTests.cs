using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurement.Core.Enums;
using QuantityMeasurement.Core.Models;

namespace QuantityMeasurement.Tests
{
    [TestClass]
    public class QuantityLengthTests
    {
        [TestMethod]
        public void Yard_To_Yard_SameValue_ShouldReturnTrue()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.YARDS);
            var q2 = new QuantityLength(1.0, LengthUnit.YARDS);

            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void Yard_To_Feet_ShouldReturnTrue()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.YARDS);
            var q2 = new QuantityLength(3.0, LengthUnit.FEET);

            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void Yard_To_Inches_ShouldReturnTrue()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.YARDS);
            var q2 = new QuantityLength(36.0, LengthUnit.INCHES);

            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void Centimeter_To_Inches_ShouldReturnTrue()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.CENTIMETERS);
            var q2 = new QuantityLength(0.393701, LengthUnit.INCHES);

            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void Yard_To_Feet_NotEqual_ShouldReturnFalse()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.YARDS);
            var q2 = new QuantityLength(2.0, LengthUnit.FEET);

            Assert.IsFalse(q1.Equals(q2));
        }

        [TestMethod]
        public void Complex_MultiUnit_ShouldReturnTrue()
        {
            var yard = new QuantityLength(2.0, LengthUnit.YARDS);
            var feet = new QuantityLength(6.0, LengthUnit.FEET);
            var inches = new QuantityLength(72.0, LengthUnit.INCHES);

            Assert.IsTrue(yard.Equals(feet));
            Assert.IsTrue(feet.Equals(inches));
            Assert.IsTrue(yard.Equals(inches));
        }

        [TestMethod]
        public void SameReference_ShouldReturnTrue()
        {
            var q = new QuantityLength(2.0, LengthUnit.YARDS);
            Assert.IsTrue(q.Equals(q));
        }

        [TestMethod]
        public void NullComparison_ShouldReturnFalse()
        {
            var q = new QuantityLength(2.0, LengthUnit.YARDS);
            Assert.IsFalse(q.Equals(null));
        }

        // =========================
        // ✅ Additional UC4 tests
        // =========================

        [TestMethod]
        public void Yard_To_Yard_DifferentValue_ShouldReturnFalse()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.YARDS);
            var q2 = new QuantityLength(2.0, LengthUnit.YARDS);

            Assert.IsFalse(q1.Equals(q2));
        }

        [TestMethod]
        public void Symmetry_Yard_And_Feet_ShouldReturnTrueBothWays()
        {
            var yard = new QuantityLength(1.0, LengthUnit.YARDS);
            var feet = new QuantityLength(3.0, LengthUnit.FEET);

            Assert.IsTrue(yard.Equals(feet));
            Assert.IsTrue(feet.Equals(yard));
        }

        [TestMethod]
        public void Symmetry_Cm_And_Inches_ShouldReturnTrueBothWays()
        {
            var cm = new QuantityLength(1.0, LengthUnit.CENTIMETERS);
            var inches = new QuantityLength(0.393701, LengthUnit.INCHES);

            Assert.IsTrue(cm.Equals(inches));
            Assert.IsTrue(inches.Equals(cm));
        }

        [TestMethod]
        public void Feet_To_Inches_ShouldReturnTrue()
        {
            var feet = new QuantityLength(1.0, LengthUnit.FEET);
            var inches = new QuantityLength(12.0, LengthUnit.INCHES);

            Assert.IsTrue(feet.Equals(inches));
        }

        [TestMethod]
        public void Inches_To_Feet_ShouldReturnTrue()
        {
            var inches = new QuantityLength(12.0, LengthUnit.INCHES);
            var feet = new QuantityLength(1.0, LengthUnit.FEET);

            Assert.IsTrue(inches.Equals(feet));
        }

        [TestMethod]
        public void Cm_To_Cm_SameValue_ShouldReturnTrue()
        {
            var q1 = new QuantityLength(2.0, LengthUnit.CENTIMETERS);
            var q2 = new QuantityLength(2.0, LengthUnit.CENTIMETERS);

            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void Cm_To_Cm_DifferentValue_ShouldReturnFalse()
        {
            var q1 = new QuantityLength(2.0, LengthUnit.CENTIMETERS);
            var q2 = new QuantityLength(3.0, LengthUnit.CENTIMETERS);

            Assert.IsFalse(q1.Equals(q2));
        }

        [TestMethod]
        public void Cm_To_Feet_NotEqual_ShouldReturnFalse()
        {
            var cm = new QuantityLength(1.0, LengthUnit.CENTIMETERS);
            var feet = new QuantityLength(1.0, LengthUnit.FEET);

            Assert.IsFalse(cm.Equals(feet));
        }

        [TestMethod]
        public void Cm_To_Yards_NotEqual_ShouldReturnFalse()
        {
            var cm = new QuantityLength(1.0, LengthUnit.CENTIMETERS);
            var yards = new QuantityLength(1.0, LengthUnit.YARDS);

            Assert.IsFalse(cm.Equals(yards));
        }

        [TestMethod]
        public void TransitiveProperty_Yard_Feet_Inches_ShouldReturnTrue()
        {
            var yard = new QuantityLength(1.0, LengthUnit.YARDS);
            var feet = new QuantityLength(3.0, LengthUnit.FEET);
            var inches = new QuantityLength(36.0, LengthUnit.INCHES);

            Assert.IsTrue(yard.Equals(feet));
            Assert.IsTrue(feet.Equals(inches));
            Assert.IsTrue(yard.Equals(inches));
        }

        [TestMethod]
        public void Precision_CloseValues_ShouldReturnTrue()
        {
            // This assumes your Equals uses an epsilon tolerance.
            // Example: 2.54 cm ~ 1 inch.
            var cm = new QuantityLength(2.54, LengthUnit.CENTIMETERS);
            var inch = new QuantityLength(1.0, LengthUnit.INCHES);

            Assert.IsTrue(cm.Equals(inch));
        }

        [TestMethod]
        public void Precision_NotCloseEnough_ShouldReturnFalse()
        {
            var inches = new QuantityLength(1.0, LengthUnit.INCHES);
            var inchesSlightlyOff = new QuantityLength(1.1, LengthUnit.INCHES);

            Assert.IsFalse(inches.Equals(inchesSlightlyOff));
        }

        [TestMethod]
        public void Equals_ObjectOverload_WithSameValues_ShouldReturnTrue()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.YARDS);
            object q2 = new QuantityLength(3.0, LengthUnit.FEET);

            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void Equals_ObjectOverload_WithDifferentType_ShouldReturnFalse()
        {
            var q = new QuantityLength(1.0, LengthUnit.YARDS);

            Assert.IsFalse(q.Equals("not a QuantityLength"));
        }


    }
}