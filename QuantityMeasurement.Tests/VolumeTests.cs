using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurement.Core.Enums;
using QuantityMeasurement.Core.Models;
using System;

namespace QuantityMeasurement.Tests
{
    [TestClass]
    public class VolumeTests
    {
        private const double Eps = 1e-6;

        // ---------------- Existing tests (keep yours) ----------------

        [TestMethod]
        public void Equality_LitreToMillilitre_ShouldReturnTrue()
        {
            var v1 = new Quantity<VolumeMeasurable>(1.0, new VolumeMeasurable(VolumeUnit.LITRE));
            var v2 = new Quantity<VolumeMeasurable>(1000.0, new VolumeMeasurable(VolumeUnit.MILLILITRE));

            Assert.IsTrue(v1.Equals(v2));
        }

        [TestMethod]
        public void Conversion_LitreToMillilitre_ShouldReturn1000()
        {
            var v1 = new Quantity<VolumeMeasurable>(1.0, new VolumeMeasurable(VolumeUnit.LITRE));
            var converted = v1.ConvertTo(new VolumeMeasurable(VolumeUnit.MILLILITRE));

            Assert.AreEqual(1000.0, converted.Value, Eps);
            Assert.AreEqual(VolumeUnit.MILLILITRE, converted.Unit.Unit);
        }

        [TestMethod]
        public void Conversion_GallonToLitre_ShouldBeAccurate()
        {
            var g = new Quantity<VolumeMeasurable>(1.0, new VolumeMeasurable(VolumeUnit.GALLON));
            var litres = g.ConvertTo(new VolumeMeasurable(VolumeUnit.LITRE));

            Assert.AreEqual(3.78541, litres.Value, 1e-6);
        }

        [TestMethod]
        public void Addition_LitrePlusMillilitre_DefaultUnitLitre()
        {
            var a = new Quantity<VolumeMeasurable>(1.0, new VolumeMeasurable(VolumeUnit.LITRE));
            var b = new Quantity<VolumeMeasurable>(1000.0, new VolumeMeasurable(VolumeUnit.MILLILITRE));

            var sum = a.Add(b);

            Assert.AreEqual(2.0, sum.Value, Eps);
            Assert.AreEqual(VolumeUnit.LITRE, sum.Unit.Unit);
        }

        [TestMethod]
        public void Addition_ExplicitTarget_Millilitre()
        {
            var a = new Quantity<VolumeMeasurable>(1.0, new VolumeMeasurable(VolumeUnit.LITRE));
            var b = new Quantity<VolumeMeasurable>(1.0, new VolumeMeasurable(VolumeUnit.GALLON));

            var sum = a.Add(b, new VolumeMeasurable(VolumeUnit.MILLILITRE));

            // 1 L + 3.78541 L = 4.78541 L = 4785.41 mL
            Assert.AreEqual(4785.41, sum.Value, 1e-5);
            Assert.AreEqual(VolumeUnit.MILLILITRE, sum.Unit.Unit);
        }

        [TestMethod]
        public void CrossCategory_VolumeVsLength_ShouldReturnFalse()
        {
            var v = new Quantity<VolumeMeasurable>(1.0, new VolumeMeasurable(VolumeUnit.LITRE));
            var l = new Quantity<LengthMeasurable>(1.0, new LengthMeasurable(LengthUnit.FEET));

            Assert.IsFalse(v.Equals(l));
        }

        // ---------------- Additional tests ----------------

        [TestMethod]
        public void Equals_NullObject_ShouldReturnFalse()
        {
            var v = new Quantity<VolumeMeasurable>(1.0, new VolumeMeasurable(VolumeUnit.LITRE));
            Assert.IsFalse(v.Equals(null));
        }

        [TestMethod]
        public void Equals_DifferentType_ShouldReturnFalse()
        {
            var v = new Quantity<VolumeMeasurable>(1.0, new VolumeMeasurable(VolumeUnit.LITRE));
            Assert.IsFalse(v.Equals("not a quantity"));
        }

        [TestMethod]
        public void Equals_SameReference_ShouldReturnTrue()
        {
            var v = new Quantity<VolumeMeasurable>(1.0, new VolumeMeasurable(VolumeUnit.LITRE));
            Assert.IsTrue(v.Equals(v));
        }

        [TestMethod]
        public void Equality_WithinEpsilon_ShouldReturnTrue()
        {
            var a = new Quantity<VolumeMeasurable>(1.0000000, new VolumeMeasurable(VolumeUnit.LITRE));
            var b = new Quantity<VolumeMeasurable>(1.0000001, new VolumeMeasurable(VolumeUnit.LITRE)); // diff 1e-7

            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void Equality_OutsideEpsilon_ShouldReturnFalse()
        {
            var a = new Quantity<VolumeMeasurable>(1.0, new VolumeMeasurable(VolumeUnit.LITRE));
            var b = new Quantity<VolumeMeasurable>(1.0001, new VolumeMeasurable(VolumeUnit.LITRE)); // diff 1e-4

            Assert.IsFalse(a.Equals(b));
        }

        [TestMethod]
        public void Convert_SameUnit_ShouldKeepValue()
        {
            var v = new Quantity<VolumeMeasurable>(2.5, new VolumeMeasurable(VolumeUnit.LITRE));

            var converted = v.ConvertTo(new VolumeMeasurable(VolumeUnit.LITRE));

            Assert.AreEqual(2.5, converted.Value, Eps);
            Assert.AreEqual(VolumeUnit.LITRE, converted.Unit.Unit);
        }

        [TestMethod]
        public void Convert_RoundTrip_LitreToMillilitreToLitre_ShouldReturnOriginal()
        {
            var v = new Quantity<VolumeMeasurable>(1.234, new VolumeMeasurable(VolumeUnit.LITRE));

            var back = v.ConvertTo(new VolumeMeasurable(VolumeUnit.MILLILITRE))
                        .ConvertTo(new VolumeMeasurable(VolumeUnit.LITRE));

            Assert.IsTrue(v.Equals(back));
        }

        [TestMethod]
        public void Convert_RoundTrip_GallonToLitreToGallon_ShouldReturnOriginalApproximately()
        {
            var g = new Quantity<VolumeMeasurable>(2.0, new VolumeMeasurable(VolumeUnit.GALLON));

            var back = g.ConvertTo(new VolumeMeasurable(VolumeUnit.LITRE))
                        .ConvertTo(new VolumeMeasurable(VolumeUnit.GALLON));

            Assert.AreEqual(2.0, back.Value, 1e-6);
            Assert.AreEqual(VolumeUnit.GALLON, back.Unit.Unit);
        }

        [TestMethod]
        public void Add_Zero_ShouldReturnSameValue()
        {
            var a = new Quantity<VolumeMeasurable>(1.0, new VolumeMeasurable(VolumeUnit.LITRE));
            var zero = new Quantity<VolumeMeasurable>(0.0, new VolumeMeasurable(VolumeUnit.MILLILITRE));

            var sum = a.Add(zero);

            Assert.AreEqual(1.0, sum.Value, Eps);
            Assert.AreEqual(VolumeUnit.LITRE, sum.Unit.Unit);
        }

        [TestMethod]
        public void Add_Negative_ShouldWorkCorrectly()
        {
            var a = new Quantity<VolumeMeasurable>(2.0, new VolumeMeasurable(VolumeUnit.LITRE));
            var b = new Quantity<VolumeMeasurable>(-500.0, new VolumeMeasurable(VolumeUnit.MILLILITRE)); // -0.5 L

            var sum = a.Add(b);

            Assert.AreEqual(1.5, sum.Value, 1e-6);
            Assert.AreEqual(VolumeUnit.LITRE, sum.Unit.Unit);
        }

        [TestMethod]
        public void Add_TargetUnit_Gallon_ShouldReturnInGallons()
        {
            var a = new Quantity<VolumeMeasurable>(1.0, new VolumeMeasurable(VolumeUnit.LITRE));
            var b = new Quantity<VolumeMeasurable>(1.0, new VolumeMeasurable(VolumeUnit.LITRE));

            var sum = a.Add(b, new VolumeMeasurable(VolumeUnit.GALLON));

            // 2 L -> 0.528344... gallons (depends on your conversion constant)
            Assert.AreEqual(0.528344, sum.Value, 1e-6);
            Assert.AreEqual(VolumeUnit.GALLON, sum.Unit.Unit);
        }

        [TestMethod]
        public void GetHashCode_EqualObjects_ShouldHaveSameHashCode()
        {
            var a = new Quantity<VolumeMeasurable>(1.0, new VolumeMeasurable(VolumeUnit.LITRE));
            var b = new Quantity<VolumeMeasurable>(1000.0, new VolumeMeasurable(VolumeUnit.MILLILITRE));

            Assert.IsTrue(a.Equals(b));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [TestMethod]
        public void Equals_ShouldBeSymmetric()
        {
            var a = new Quantity<VolumeMeasurable>(1.0, new VolumeMeasurable(VolumeUnit.LITRE));
            var b = new Quantity<VolumeMeasurable>(1000.0, new VolumeMeasurable(VolumeUnit.MILLILITRE));

            Assert.AreEqual(a.Equals(b), b.Equals(a));
        }

        [TestMethod]
        public void Equals_ShouldBeTransitive()
        {
            var a = new Quantity<VolumeMeasurable>(1.0, new VolumeMeasurable(VolumeUnit.LITRE));
            var b = new Quantity<VolumeMeasurable>(1000.0, new VolumeMeasurable(VolumeUnit.MILLILITRE));
            var c = new Quantity<VolumeMeasurable>(0.264172, new VolumeMeasurable(VolumeUnit.GALLON)); // ~1 L

            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(b.Equals(c));
            Assert.IsTrue(a.Equals(c));
        }

        [TestMethod]
        public void Equals_DifferentUnitsSameValue_ShouldReturnTrue()
        {
            var a = new Quantity<VolumeMeasurable>(2500.0, new VolumeMeasurable(VolumeUnit.MILLILITRE));
            var b = new Quantity<VolumeMeasurable>(2.5, new VolumeMeasurable(VolumeUnit.LITRE));

            Assert.IsTrue(a.Equals(b));
        }
    }
}