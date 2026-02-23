using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using QuantityMeasurement.Core.Enums;
using QuantityMeasurement.Core.Models;

namespace QuantityMeasurement.Tests
{
    [TestClass]
    public class QuantityLengthTests
    {
        private const double EPSILON = 1e-6;

        // ---------------- BASIC CONVERSION ----------------

        [TestMethod]
        public void Convert_FeetToInches()
        {
            double result = QuantityLength.Convert(1.0, LengthUnit.FEET, LengthUnit.INCHES);
            Assert.AreEqual(12.0, result, EPSILON);
        }

        [TestMethod]
        public void Convert_InchesToFeet()
        {
            double result = QuantityLength.Convert(24.0, LengthUnit.INCHES, LengthUnit.FEET);
            Assert.AreEqual(2.0, result, EPSILON);
        }

        [TestMethod]
        public void Convert_YardsToInches()
        {
            double result = QuantityLength.Convert(1.0, LengthUnit.YARDS, LengthUnit.INCHES);
            Assert.AreEqual(36.0, result, EPSILON);
        }

        [TestMethod]
        public void Convert_InchesToYards()
        {
            double result = QuantityLength.Convert(72.0, LengthUnit.INCHES, LengthUnit.YARDS);
            Assert.AreEqual(2.0, result, EPSILON);
        }

        [TestMethod]
        public void Convert_CentimetersToInches()
        {
            double result = QuantityLength.Convert(2.54, LengthUnit.CENTIMETERS, LengthUnit.INCHES);
            Assert.AreEqual(1.0, result, EPSILON);
        }

        [TestMethod]
        public void Convert_FeetToYards()
        {
            double result = QuantityLength.Convert(6.0, LengthUnit.FEET, LengthUnit.YARDS);
            Assert.AreEqual(2.0, result, EPSILON);
        }

        [TestMethod]
        public void Convert_YardsToCentimeters()
        {
            // 1 yard = 36 inches; 1 inch = 2.54 cm => 1 yard = 91.44 cm
            double result = QuantityLength.Convert(1.0, LengthUnit.YARDS, LengthUnit.CENTIMETERS);
            Assert.AreEqual(91.44, result, EPSILON);
        }

        [TestMethod]
        public void Convert_CentimetersToFeet()
        {
            // 30.48 cm = 12 inches = 1 foot
            double result = QuantityLength.Convert(30.48, LengthUnit.CENTIMETERS, LengthUnit.FEET);
            Assert.AreEqual(1.0, result, EPSILON);
        }

        // ---------------- SAME UNIT ----------------

        [TestMethod]
        public void Convert_SameUnit_ReturnsSameValue()
        {
            double result = QuantityLength.Convert(5.0, LengthUnit.FEET, LengthUnit.FEET);
            Assert.AreEqual(5.0, result, EPSILON);
        }

        // ---------------- ZERO VALUE ----------------

        [TestMethod]
        public void Convert_ZeroValue()
        {
            double result = QuantityLength.Convert(0.0, LengthUnit.FEET, LengthUnit.INCHES);
            Assert.AreEqual(0.0, result, EPSILON);
        }

  

        // ---------------- ROUND TRIP ----------------

        [TestMethod]
        public void Convert_RoundTrip_PreservesValue()
        {
            double original = 5.0;

            double converted = QuantityLength.Convert(original, LengthUnit.FEET, LengthUnit.YARDS);
            double roundTrip = QuantityLength.Convert(converted, LengthUnit.YARDS, LengthUnit.FEET);

            Assert.AreEqual(original, roundTrip, EPSILON);
        }

        // ---------------- LARGE VALUE ----------------

        [TestMethod]
        public void Convert_LargeValue()
        {
            double result = QuantityLength.Convert(1000000, LengthUnit.YARDS, LengthUnit.FEET);
            Assert.AreEqual(3000000, result, EPSILON);
        }

        // ---------------- SMALL VALUE ----------------

        [TestMethod]
        public void Convert_SmallValue()
        {
            double result = QuantityLength.Convert(0.0001, LengthUnit.FEET, LengthUnit.INCHES);
            Assert.AreEqual(0.0012, result, EPSILON);
        }

        // ---------------- INVALID VALUES ----------------



        [TestMethod]
        public void Equality_YardToFeet()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.YARDS);
            var q2 = new QuantityLength(3.0, LengthUnit.FEET);

            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void Equality_FeetToYard_Symmetric()
        {
            var q1 = new QuantityLength(3.0, LengthUnit.FEET);
            var q2 = new QuantityLength(1.0, LengthUnit.YARDS);

            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void Equality_YardToInches()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.YARDS);
            var q2 = new QuantityLength(36.0, LengthUnit.INCHES);

            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void Equality_CentimeterToInch()
        {
            var q1 = new QuantityLength(2.54, LengthUnit.CENTIMETERS);
            var q2 = new QuantityLength(1.0, LengthUnit.INCHES);

            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void Equality_DifferentValues_ReturnsFalse()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.FEET);
            var q2 = new QuantityLength(2.0, LengthUnit.FEET);

            Assert.IsFalse(q1.Equals(q2));
        }

        [TestMethod]
        public void Equality_NullComparison_ReturnsFalse()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.FEET);
            Assert.IsFalse(q1.Equals(null));
        }

        [TestMethod]
        public void Equality_SameReference_ReturnsTrue()
        {
            var q1 = new QuantityLength(2.0, LengthUnit.FEET);
            Assert.IsTrue(q1.Equals(q1));
        }

        [TestMethod]
        public void Equality_TransitiveProperty_Yard_Feet_Inches()
        {
            var a = new QuantityLength(1.0, LengthUnit.YARDS);
            var b = new QuantityLength(3.0, LengthUnit.FEET);
            var c = new QuantityLength(36.0, LengthUnit.INCHES);

            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(b.Equals(c));
            Assert.IsTrue(a.Equals(c));
        }

        [TestMethod]
        public void Equality_ObjectOverload_ReturnsTrue()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.YARDS);
            object q2 = new QuantityLength(3.0, LengthUnit.FEET);

            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void Equality_ObjectOverload_WithDifferentType_ReturnsFalse()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.YARDS);
            object notAQuantity = "abc";

            Assert.IsFalse(q1.Equals(notAQuantity));
        }

        
    }
}