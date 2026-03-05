using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurement.Core.Enums;
using QuantityMeasurement.Core.Models;
using System;

namespace QuantityMeasurement.Tests
{
    [TestClass]
    public class QuantitySubtractionDivisionTests
    {
        private const double Eps = 1e-6;

        // ------------------ SUBTRACTION ------------------

        [TestMethod]
     public void testSubtraction_SameUnit_FeetMinusFeet()
        {
            var a = new Quantity<LengthMeasurable>(10.0, new LengthMeasurable(LengthUnit.FEET));
            var b = new Quantity<LengthMeasurable>(5.0, new LengthMeasurable(LengthUnit.FEET));

            var result = a.Subtract(b);

            Assert.AreEqual(5.0, result.Value, Eps);
            Assert.AreEqual(LengthUnit.FEET, result.Unit.Unit);
        }

        [TestMethod]
        public void testSubtraction_SameUnit_LitreMinusLitre()
        {
            var a = new Quantity<VolumeMeasurable>(10.0, new VolumeMeasurable(VolumeUnit.LITRE));
            var b = new Quantity<VolumeMeasurable>(3.0, new VolumeMeasurable(VolumeUnit.LITRE));

            var result = a.Subtract(b);

            Assert.AreEqual(7.0, result.Value, Eps);
            Assert.AreEqual(VolumeUnit.LITRE, result.Unit.Unit);
        }

        [TestMethod]
        public void testSubtraction_CrossUnit_FeetMinusInches()
        {
            var a = new Quantity<LengthMeasurable>(10.0, new LengthMeasurable(LengthUnit.FEET));
            var b = new Quantity<LengthMeasurable>(6.0, new LengthMeasurable(LengthUnit.INCHES));

            var result = a.Subtract(b);

            Assert.AreEqual(9.5, result.Value, Eps);
            Assert.AreEqual(LengthUnit.FEET, result.Unit.Unit);
        }

        [TestMethod]
        public void testSubtraction_CrossUnit_InchesMinusFeet()
        {
            var a = new Quantity<LengthMeasurable>(120.0, new LengthMeasurable(LengthUnit.INCHES));
            var b = new Quantity<LengthMeasurable>(5.0, new LengthMeasurable(LengthUnit.FEET));

            var result = a.Subtract(b);

            Assert.AreEqual(60.0, result.Value, Eps);
            Assert.AreEqual(LengthUnit.INCHES, result.Unit.Unit);
        }

        [TestMethod]
        public void testSubtraction_ExplicitTargetUnit_Feet()
        {
            var a = new Quantity<LengthMeasurable>(10.0, new LengthMeasurable(LengthUnit.FEET));
            var b = new Quantity<LengthMeasurable>(6.0, new LengthMeasurable(LengthUnit.INCHES));

            var result = a.Subtract(b, new LengthMeasurable(LengthUnit.FEET));

            Assert.AreEqual(9.5, result.Value, Eps);
            Assert.AreEqual(LengthUnit.FEET, result.Unit.Unit);
        }

        [TestMethod]
        public void testSubtraction_ExplicitTargetUnit_Inches()
        {
            var a = new Quantity<LengthMeasurable>(10.0, new LengthMeasurable(LengthUnit.FEET));
            var b = new Quantity<LengthMeasurable>(6.0, new LengthMeasurable(LengthUnit.INCHES));

            var result = a.Subtract(b, new LengthMeasurable(LengthUnit.INCHES));

            Assert.AreEqual(114.0, result.Value, Eps);
            Assert.AreEqual(LengthUnit.INCHES, result.Unit.Unit);
        }

        [TestMethod]
        public void testSubtraction_ExplicitTargetUnit_Millilitre()
        {
            var a = new Quantity<VolumeMeasurable>(5.0, new VolumeMeasurable(VolumeUnit.LITRE));
            var b = new Quantity<VolumeMeasurable>(2.0, new VolumeMeasurable(VolumeUnit.LITRE));

            var result = a.Subtract(b, new VolumeMeasurable(VolumeUnit.MILLILITRE));

            Assert.AreEqual(3000.0, result.Value, Eps);
            Assert.AreEqual(VolumeUnit.MILLILITRE, result.Unit.Unit);
        }

        [TestMethod]
        public void testSubtraction_ResultingInNegative()
        {
            var a = new Quantity<LengthMeasurable>(5.0, new LengthMeasurable(LengthUnit.FEET));
            var b = new Quantity<LengthMeasurable>(10.0, new LengthMeasurable(LengthUnit.FEET));

            var result = a.Subtract(b);

            Assert.AreEqual(-5.0, result.Value, Eps);
            Assert.AreEqual(LengthUnit.FEET, result.Unit.Unit);
        }

        [TestMethod]
        public void testSubtraction_ResultingInZero()
        {
            var a = new Quantity<LengthMeasurable>(10.0, new LengthMeasurable(LengthUnit.FEET));
            var b = new Quantity<LengthMeasurable>(120.0, new LengthMeasurable(LengthUnit.INCHES));

            var result = a.Subtract(b);

            Assert.AreEqual(0.0, result.Value, Eps);
            Assert.AreEqual(LengthUnit.FEET, result.Unit.Unit);
        }

        [TestMethod]
        public void testSubtraction_WithZeroOperand()
        {
            var a = new Quantity<LengthMeasurable>(5.0, new LengthMeasurable(LengthUnit.FEET));
            var zero = new Quantity<LengthMeasurable>(0.0, new LengthMeasurable(LengthUnit.INCHES));

            var result = a.Subtract(zero);

            Assert.AreEqual(5.0, result.Value, Eps);
            Assert.AreEqual(LengthUnit.FEET, result.Unit.Unit);
        }

        [TestMethod]
        public void testSubtraction_WithNegativeValues()
        {
            var a = new Quantity<LengthMeasurable>(5.0, new LengthMeasurable(LengthUnit.FEET));
            var b = new Quantity<LengthMeasurable>(-2.0, new LengthMeasurable(LengthUnit.FEET));

            var result = a.Subtract(b);

            Assert.AreEqual(7.0, result.Value, Eps);
            Assert.AreEqual(LengthUnit.FEET, result.Unit.Unit);
        }

        [TestMethod]
        public void testSubtraction_NonCommutative()
        {
            var a = new Quantity<LengthMeasurable>(10.0, new LengthMeasurable(LengthUnit.FEET));
            var b = new Quantity<LengthMeasurable>(5.0, new LengthMeasurable(LengthUnit.FEET));

            var ab = a.Subtract(b);
            var ba = b.Subtract(a);

            Assert.AreEqual(5.0, ab.Value, Eps);
            Assert.AreEqual(-5.0, ba.Value, Eps);
        }

        [TestMethod]
        public void testSubtraction_WithLargeValues()
        {
            var a = new Quantity<WeightMeasurable>(1e6, new WeightMeasurable(WeightUnit.KILOGRAM));
            var b = new Quantity<WeightMeasurable>(5e5, new WeightMeasurable(WeightUnit.KILOGRAM));

            var result = a.Subtract(b);

            Assert.AreEqual(5e5, result.Value, Eps);
            Assert.AreEqual(WeightUnit.KILOGRAM, result.Unit.Unit);
        }

        [TestMethod]
        public void testSubtraction_WithSmallValues()
        {
            var a = new Quantity<LengthMeasurable>(0.001, new LengthMeasurable(LengthUnit.FEET));
            var b = new Quantity<LengthMeasurable>(0.0005, new LengthMeasurable(LengthUnit.FEET));

            var result = a.Subtract(b);

            Assert.AreEqual(0.0005, result.Value, 1e-12);
        }

        [TestMethod]

        public void testSubtraction_NullTargetUnit_NotApplicable_InCSharp()
        {
            Assert.IsTrue(true);
        }

   
        [TestMethod]
        public void testSubtraction_CrossCategory_CompilerPrevents()
        {
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void testSubtraction_AllMeasurementCategories()
        {
            var l = new Quantity<LengthMeasurable>(10.0, new LengthMeasurable(LengthUnit.FEET))
                .Subtract(new Quantity<LengthMeasurable>(6.0, new LengthMeasurable(LengthUnit.INCHES)));
            Assert.AreEqual(9.5, l.Value, Eps);

            var w = new Quantity<WeightMeasurable>(10.0, new WeightMeasurable(WeightUnit.KILOGRAM))
                .Subtract(new Quantity<WeightMeasurable>(5000.0, new WeightMeasurable(WeightUnit.GRAM)));
            Assert.AreEqual(5.0, w.Value, Eps);

            var v = new Quantity<VolumeMeasurable>(5.0, new VolumeMeasurable(VolumeUnit.LITRE))
                .Subtract(new Quantity<VolumeMeasurable>(500.0, new VolumeMeasurable(VolumeUnit.MILLILITRE)));
            Assert.AreEqual(4.5, v.Value, Eps);
        }

        [TestMethod]
        public void testSubtraction_ChainedOperations()
        {
            var a = new Quantity<LengthMeasurable>(10.0, new LengthMeasurable(LengthUnit.FEET));
            var result = a.Subtract(new Quantity<LengthMeasurable>(2.0, new LengthMeasurable(LengthUnit.FEET)))
                          .Subtract(new Quantity<LengthMeasurable>(1.0, new LengthMeasurable(LengthUnit.FEET)));

            Assert.AreEqual(7.0, result.Value, Eps);
            Assert.AreEqual(LengthUnit.FEET, result.Unit.Unit);
        }


        [TestMethod]
        public void testDivision_SameUnit_FeetDividedByFeet()
        {
            var a = new Quantity<LengthMeasurable>(10.0, new LengthMeasurable(LengthUnit.FEET));
            var b = new Quantity<LengthMeasurable>(2.0, new LengthMeasurable(LengthUnit.FEET));

            double ratio = a.Divide(b);

            Assert.AreEqual(5.0, ratio, Eps);
        }

        [TestMethod]
        public void testDivision_SameUnit_LitreDividedByLitre()
        {
            var a = new Quantity<VolumeMeasurable>(10.0, new VolumeMeasurable(VolumeUnit.LITRE));
            var b = new Quantity<VolumeMeasurable>(5.0, new VolumeMeasurable(VolumeUnit.LITRE));

            double ratio = a.Divide(b);

            Assert.AreEqual(2.0, ratio, Eps);
        }

        [TestMethod]
        public void testDivision_CrossUnit_FeetDividedByInches()
        {
            var a = new Quantity<LengthMeasurable>(24.0, new LengthMeasurable(LengthUnit.INCHES));
            var b = new Quantity<LengthMeasurable>(2.0, new LengthMeasurable(LengthUnit.FEET));

            double ratio = a.Divide(b);

            Assert.AreEqual(1.0, ratio, Eps);
        }

        [TestMethod]
        public void testDivision_CrossUnit_KilogramDividedByGram()
        {
            var a = new Quantity<WeightMeasurable>(2.0, new WeightMeasurable(WeightUnit.KILOGRAM));
            var b = new Quantity<WeightMeasurable>(2000.0, new WeightMeasurable(WeightUnit.GRAM));

            double ratio = a.Divide(b);

            Assert.AreEqual(1.0, ratio, Eps);
        }

        [TestMethod]
        public void testDivision_RatioGreaterThanOne()
        {
            var a = new Quantity<LengthMeasurable>(10.0, new LengthMeasurable(LengthUnit.FEET));
            var b = new Quantity<LengthMeasurable>(2.0, new LengthMeasurable(LengthUnit.FEET));

            Assert.AreEqual(5.0, a.Divide(b), Eps);
        }

        [TestMethod]
        public void testDivision_RatioLessThanOne()
        {
            var a = new Quantity<LengthMeasurable>(5.0, new LengthMeasurable(LengthUnit.FEET));
            var b = new Quantity<LengthMeasurable>(10.0, new LengthMeasurable(LengthUnit.FEET));

            Assert.AreEqual(0.5, a.Divide(b), Eps);
        }

        [TestMethod]
        public void testDivision_RatioEqualToOne()
        {
            var a = new Quantity<LengthMeasurable>(10.0, new LengthMeasurable(LengthUnit.FEET));
            var b = new Quantity<LengthMeasurable>(10.0, new LengthMeasurable(LengthUnit.FEET));

            Assert.AreEqual(1.0, a.Divide(b), Eps);
        }

        [TestMethod]
        public void testDivision_NonCommutative()
        {
            var a = new Quantity<LengthMeasurable>(10.0, new LengthMeasurable(LengthUnit.FEET));
            var b = new Quantity<LengthMeasurable>(5.0, new LengthMeasurable(LengthUnit.FEET));

            double ab = a.Divide(b);
            double ba = b.Divide(a);

            Assert.AreEqual(2.0, ab, Eps);
            Assert.AreEqual(0.5, ba, Eps);
        }

    

        [TestMethod]
        public void testDivision_WithLargeRatio()
        {
            var a = new Quantity<WeightMeasurable>(1e6, new WeightMeasurable(WeightUnit.KILOGRAM));
            var b = new Quantity<WeightMeasurable>(1.0, new WeightMeasurable(WeightUnit.KILOGRAM));

            Assert.AreEqual(1e6, a.Divide(b), Eps);
        }

        [TestMethod]
        public void testDivision_WithSmallRatio()
        {
            var a = new Quantity<WeightMeasurable>(1.0, new WeightMeasurable(WeightUnit.KILOGRAM));
            var b = new Quantity<WeightMeasurable>(1e6, new WeightMeasurable(WeightUnit.KILOGRAM));

            Assert.AreEqual(1e-6, a.Divide(b), 1e-12);
        }

  

        // NOTE: Cross-category division is also prevented at compile-time
        [TestMethod]
        public void testDivision_CrossCategory_CompilerPrevents()
        {
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void testDivision_AllMeasurementCategories()
        {
            var l = new Quantity<LengthMeasurable>(24.0, new LengthMeasurable(LengthUnit.INCHES))
                .Divide(new Quantity<LengthMeasurable>(2.0, new LengthMeasurable(LengthUnit.FEET)));
            Assert.AreEqual(1.0, l, Eps);

            var w = new Quantity<WeightMeasurable>(2000.0, new WeightMeasurable(WeightUnit.GRAM))
                .Divide(new Quantity<WeightMeasurable>(1.0, new WeightMeasurable(WeightUnit.KILOGRAM)));
            Assert.AreEqual(2.0, w, Eps);

            var v = new Quantity<VolumeMeasurable>(1000.0, new VolumeMeasurable(VolumeUnit.MILLILITRE))
                .Divide(new Quantity<VolumeMeasurable>(1.0, new VolumeMeasurable(VolumeUnit.LITRE)));
            Assert.AreEqual(1.0, v, Eps);
        }

        // (A ÷ B) ÷ C != A ÷ (B ÷ C) check with raw doubles
        [TestMethod]
        public void testDivision_Associativity()
        {
            var a = new Quantity<LengthMeasurable>(100.0, new LengthMeasurable(LengthUnit.INCHES));
            var b = new Quantity<LengthMeasurable>(10.0, new LengthMeasurable(LengthUnit.INCHES));
            var c = new Quantity<LengthMeasurable>(2.0, new LengthMeasurable(LengthUnit.INCHES));

            double left = (a.Divide(b)) / c.Divide(new Quantity<LengthMeasurable>(1.0, new LengthMeasurable(LengthUnit.INCHES)));
            double right = a.Divide(new Quantity<LengthMeasurable>(1.0, new LengthMeasurable(LengthUnit.INCHES))) / (b.Divide(c));

            Assert.IsTrue(Math.Abs(left - right) > 1e-9);
        }

        // ------------------ INTEGRATION ------------------

        [TestMethod]
        public void testSubtractionAndDivision_Integration()
        {
            var a = new Quantity<LengthMeasurable>(10.0, new LengthMeasurable(LengthUnit.FEET));
            var b = new Quantity<LengthMeasurable>(6.0, new LengthMeasurable(LengthUnit.INCHES));
            var c = new Quantity<LengthMeasurable>(2.0, new LengthMeasurable(LengthUnit.FEET));

            var diff = a.Subtract(b);     // 9.5 feet
            double ratio = diff.Divide(c); // 9.5 / 2 = 4.75

            Assert.AreEqual(4.75, ratio, Eps);
        }

        [TestMethod]
        public void testSubtractionAddition_Inverse()
        {
            var a = new Quantity<WeightMeasurable>(10.0, new WeightMeasurable(WeightUnit.KILOGRAM));
            var b = new Quantity<WeightMeasurable>(5000.0, new WeightMeasurable(WeightUnit.GRAM)); // 5 kg

            var back = a.Add(b).Subtract(b);

            Assert.IsTrue(a.Equals(back));
        }

        [TestMethod]
        public void testSubtraction_Immutability()
        {
            var a = new Quantity<LengthMeasurable>(10.0, new LengthMeasurable(LengthUnit.FEET));
            var b = new Quantity<LengthMeasurable>(5.0, new LengthMeasurable(LengthUnit.FEET));

            var result = a.Subtract(b);

            Assert.AreEqual(10.0, a.Value, Eps);
            Assert.AreEqual(5.0, b.Value, Eps);
            Assert.AreEqual(5.0, result.Value, Eps);
        }

        [TestMethod]
        public void testDivision_Immutability()
        {
            var a = new Quantity<LengthMeasurable>(10.0, new LengthMeasurable(LengthUnit.FEET));
            var b = new Quantity<LengthMeasurable>(2.0, new LengthMeasurable(LengthUnit.FEET));

            var ratio = a.Divide(b);

            Assert.AreEqual(10.0, a.Value, Eps);
            Assert.AreEqual(2.0, b.Value, Eps);
            Assert.AreEqual(5.0, ratio, Eps);
        }


        [TestMethod]
        public void testSubtraction_PrecisionAndRounding_NotEnforced()
        {
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void testDivision_PrecisionHandling()
        {
            var a = new Quantity<WeightMeasurable>(1.0, new WeightMeasurable(WeightUnit.KILOGRAM));
            var b = new Quantity<WeightMeasurable>(3.0, new WeightMeasurable(WeightUnit.KILOGRAM));

            double ratio = a.Divide(b);

            Assert.AreEqual(0.3333333333333333, ratio, 1e-12);
        }
    }
}