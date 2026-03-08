using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurement.Core.Enums;
using QuantityMeasurement.Core.Models;
using System;

namespace QuantityMeasurement.Tests
{
    [TestClass]
    public class QuantityMeasurementTests
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
public void testDivision_SameUnit_FeetByFeet()
{
var a = new Quantity<LengthMeasurable>(10.0, new LengthMeasurable(LengthUnit.FEET));
var b = new Quantity<LengthMeasurable>(5.0, new LengthMeasurable(LengthUnit.FEET));


var result = a.Divide(b);

Assert.AreEqual(2.0, result, Eps);


}

[TestMethod]
public void testDivision_SameUnit_LitreByLitre()
{
var a = new Quantity<VolumeMeasurable>(12.0, new VolumeMeasurable(VolumeUnit.LITRE));
var b = new Quantity<VolumeMeasurable>(3.0, new VolumeMeasurable(VolumeUnit.LITRE));


var result = a.Divide(b);

Assert.AreEqual(4.0, result, Eps);


}

[TestMethod]
public void testDivision_CrossUnit_FeetByInches()
{
var a = new Quantity<LengthMeasurable>(10.0, new LengthMeasurable(LengthUnit.FEET));
var b = new Quantity<LengthMeasurable>(24.0, new LengthMeasurable(LengthUnit.INCHES));


var result = a.Divide(b);

Assert.AreEqual(5.0, result, Eps);


}








[TestMethod]
public void testImmutability_AfterSubtract()
{
var a = new Quantity<LengthMeasurable>(10.0, new LengthMeasurable(LengthUnit.FEET));
var b = new Quantity<LengthMeasurable>(4.0, new LengthMeasurable(LengthUnit.FEET));


var result = a.Subtract(b);

Assert.AreEqual(10.0, a.Value, Eps);
Assert.AreEqual(4.0, b.Value, Eps);


}

[TestMethod]
public void testImmutability_AfterDivide()
{
var a = new Quantity<LengthMeasurable>(20.0, new LengthMeasurable(LengthUnit.FEET));
var b = new Quantity<LengthMeasurable>(4.0, new LengthMeasurable(LengthUnit.FEET));


var result = a.Divide(b);

Assert.AreEqual(20.0, a.Value, Eps);
Assert.AreEqual(4.0, b.Value, Eps);


}

[TestMethod]
public void testArithmetic_Chain_Operations()
{
var q1 = new Quantity<LengthMeasurable>(20.0, new LengthMeasurable(LengthUnit.FEET));
var q2 = new Quantity<LengthMeasurable>(5.0, new LengthMeasurable(LengthUnit.FEET));
var q3 = new Quantity<LengthMeasurable>(3.0, new LengthMeasurable(LengthUnit.FEET));
var q4 = new Quantity<LengthMeasurable>(2.0, new LengthMeasurable(LengthUnit.FEET));


var result = q1.Subtract(q2).Subtract(q3).Divide(q4);

Assert.AreEqual(6.0, result, Eps);


}

[TestMethod]
public void testAllOperations_AcrossCategories_LengthAndVolume()
{
var lengthA = new Quantity<LengthMeasurable>(10.0, new LengthMeasurable(LengthUnit.FEET));
var lengthB = new Quantity<LengthMeasurable>(5.0, new LengthMeasurable(LengthUnit.FEET));


var volumeA = new Quantity<VolumeMeasurable>(10.0, new VolumeMeasurable(VolumeUnit.LITRE));
var volumeB = new Quantity<VolumeMeasurable>(2.0, new VolumeMeasurable(VolumeUnit.LITRE));

var lengthResult = lengthA.Subtract(lengthB);
var volumeResult = volumeA.Subtract(volumeB);

Assert.AreEqual(5.0, lengthResult.Value, Eps);
Assert.AreEqual(8.0, volumeResult.Value, Eps);

}

    }
}