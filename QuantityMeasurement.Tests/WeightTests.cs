using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurement.Core.Enums;
using QuantityMeasurement.Core.Models;
using System;

namespace QuantityMeasurement.Tests
{
    [TestClass]
    public class WeightTests
    {
        private const double Eps = 1e-6;

        [TestMethod]
        public void KilogramEquality_SameValue_ShouldReturnTrue()
        {
            Assert.IsTrue(new QuantityWeight(1.0, WeightUnit.KILOGRAM)
                .Equals(new QuantityWeight(1.0, WeightUnit.KILOGRAM)));
        }

        [TestMethod]
        public void KilogramToGram_Equivalent_ShouldReturnTrue()
        {
            Assert.IsTrue(new QuantityWeight(1.0, WeightUnit.KILOGRAM)
                .Equals(new QuantityWeight(1000.0, WeightUnit.GRAM)));
        }

        [TestMethod]
        public void PoundToKilogram_Equivalent_ShouldReturnTrue()
        {
            var kg = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
            var lb = new QuantityWeight(2.2046226218, WeightUnit.POUND); // ~1 kg
            Assert.IsTrue(kg.Equals(lb));
        }

        [TestMethod]
        public void Convert_KgToGram_ShouldReturn1000()
        {
            var converted = new QuantityWeight(1.0, WeightUnit.KILOGRAM).ConvertTo(WeightUnit.GRAM);
            Assert.AreEqual(1000.0, converted.Value, Eps);
            Assert.AreEqual(WeightUnit.GRAM, converted.Unit);
        }

        [TestMethod]
        public void Convert_PoundToKg_ShouldBeAccurate()
        {
            var converted = new QuantityWeight(2.0, WeightUnit.POUND).ConvertTo(WeightUnit.KILOGRAM);
            Assert.AreEqual(0.907184, converted.Value, 1e-6);
        }

        [TestMethod]
        public void Add_KgPlusGram_DefaultUnitKg()
        {
            var a = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
            var b = new QuantityWeight(1000.0, WeightUnit.GRAM);

            var sum = a.Add(b);

            Assert.AreEqual(2.0, sum.Value, Eps);
            Assert.AreEqual(WeightUnit.KILOGRAM, sum.Unit);
        }

        [TestMethod]
        public void Add_ExplicitTarget_Gram()
        {
            var a = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
            var b = new QuantityWeight(1000.0, WeightUnit.GRAM);

            var sum = a.Add(b, WeightUnit.GRAM);

            Assert.AreEqual(2000.0, sum.Value, Eps);
            Assert.AreEqual(WeightUnit.GRAM, sum.Unit);
        }

        [TestMethod]
        public void WeightVsLength_ShouldNotBeEqual()
        {
            var w = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
            var l = new QuantityMeasurement.Core.Models.QuantityLength(1.0, QuantityMeasurement.Core.Enums.LengthUnit.FEET);

            Assert.IsFalse(w.Equals(l));
        }


        [TestMethod]
        public void Equals_NullObject_ShouldReturnFalse()
        {
            var w = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
            Assert.IsFalse(w.Equals(null));
        }

        [TestMethod]
        public void Equals_DifferentType_ShouldReturnFalse()
        {
            var w = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
            Assert.IsFalse(w.Equals("not a weight"));
        }

        [TestMethod]
        public void Equals_SameReference_ShouldReturnTrue()
        {
            var w = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
            Assert.IsTrue(w.Equals(w));
        }

        [TestMethod]
        public void Equals_WithinEpsilon_ShouldReturnTrue()
        {
            var a = new QuantityWeight(1.0000000, WeightUnit.KILOGRAM);
            var b = new QuantityWeight(1.0000001, WeightUnit.KILOGRAM); // diff 1e-7 <= 1e-6

            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void Equals_OutsideEpsilon_ShouldReturnFalse()
        {
            var a = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
            var b = new QuantityWeight(1.0001, WeightUnit.KILOGRAM); // diff 1e-4 > 1e-6

            Assert.IsFalse(a.Equals(b));
        }

        [TestMethod]
        public void Ctor_NaN_ShouldThrowArgumentException()
        {
            try
            {
                _ = new QuantityWeight(double.NaN, WeightUnit.KILOGRAM);
                Assert.Fail("Expected ArgumentException was not thrown.");
            }
            catch (ArgumentException)
            {
                // pass
            }
        }

        [TestMethod]
        public void Ctor_PositiveInfinity_ShouldThrowArgumentException()
        {
            try
            {
                _ = new QuantityWeight(double.PositiveInfinity, WeightUnit.KILOGRAM);
                Assert.Fail("Expected ArgumentException was not thrown.");
            }
            catch (ArgumentException)
            {
                // pass
            }
        }

        [TestMethod]
        public void Ctor_NegativeInfinity_ShouldThrowArgumentException()
        {
            try
            {
                _ = new QuantityWeight(double.NegativeInfinity, WeightUnit.KILOGRAM);
                Assert.Fail("Expected ArgumentException was not thrown.");
            }
            catch (ArgumentException)
            {
                // pass
            }
        }

        [TestMethod]
        public void Add_NullOther_ShouldThrowArgumentNullException()
        {
            var a = new QuantityWeight(1.0, WeightUnit.KILOGRAM);

            try
            {
                _ = a.Add(null!);
                Assert.Fail("Expected ArgumentNullException was not thrown.");
            }
            catch (ArgumentNullException)
            {
                // pass
            }
        }

        [TestMethod]
        public void Convert_SameUnit_ShouldKeepValueAndUnit()
        {
            var w = new QuantityWeight(2.5, WeightUnit.KILOGRAM);

            var converted = w.ConvertTo(WeightUnit.KILOGRAM);

            Assert.AreEqual(2.5, converted.Value, Eps);
            Assert.AreEqual(WeightUnit.KILOGRAM, converted.Unit);
        }

        [TestMethod]
        public void Convert_RoundTrip_KgToGramToKg_ShouldReturnOriginal()
        {
            var w = new QuantityWeight(1.234, WeightUnit.KILOGRAM);

            var back = w.ConvertTo(WeightUnit.GRAM).ConvertTo(WeightUnit.KILOGRAM);

            Assert.IsTrue(w.Equals(back));
        }

        [TestMethod]
        public void Add_Zero_ShouldReturnSameQuantityInTargetUnit()
        {
            var a = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
            var zero = new QuantityWeight(0.0, WeightUnit.GRAM);

            var sum = a.Add(zero, WeightUnit.KILOGRAM);

            Assert.AreEqual(1.0, sum.Value, Eps);
            Assert.AreEqual(WeightUnit.KILOGRAM, sum.Unit);
        }

        [TestMethod]
        public void Add_NegativeValues_ShouldWorkCorrectly()
        {
            var a = new QuantityWeight(2.0, WeightUnit.KILOGRAM);
            var b = new QuantityWeight(-500.0, WeightUnit.GRAM); // -0.5 kg

            var sum = a.Add(b);

            Assert.AreEqual(1.5, sum.Value, Eps);
            Assert.AreEqual(WeightUnit.KILOGRAM, sum.Unit);
        }

        [TestMethod]
        public void GetHashCode_EqualObjects_ShouldHaveSameHashCode()
        {
            var a = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
            var b = new QuantityWeight(1000.0, WeightUnit.GRAM);

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [TestMethod]
        public void Equals_ShouldBeSymmetric()
        {
            var a = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
            var b = new QuantityWeight(1000.0, WeightUnit.GRAM);

            Assert.AreEqual(a.Equals(b), b.Equals(a));
        }

        [TestMethod]
        public void Equals_ShouldBeTransitive()
        {
            var a = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
            var b = new QuantityWeight(1000.0, WeightUnit.GRAM);
            var c = new QuantityWeight(2.2046226218, WeightUnit.POUND); // ~1 kg

            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(b.Equals(c));
            Assert.IsTrue(a.Equals(c));
        }
    }
}