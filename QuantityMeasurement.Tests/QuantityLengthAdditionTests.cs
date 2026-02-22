using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using QuantityMeasurement.Core.Enums;
using QuantityMeasurement.Core.Models;

namespace QuantityMeasurement.Tests
{
[TestClass]
public class QuantityLengthAdditionTests
{
    [TestMethod]
    public void testAddition_SameUnit_FeetPlusFeet()
    {
        var l1 = new QuantityLength(1, LengthUnit.FEET);
        var l2 = new QuantityLength(2, LengthUnit.FEET);

        var result = l1.Add(l2);

        Assert.AreEqual(new QuantityLength(3, LengthUnit.FEET), result);
    }

    [TestMethod]
    public void testAddition_CrossUnit_FeetPlusInches()
    {
        var l1 = new QuantityLength(1, LengthUnit.FEET);
        var l2 = new QuantityLength(12, LengthUnit.INCHES);

        var result = l1.Add(l2);

        Assert.AreEqual(new QuantityLength(2, LengthUnit.FEET), result);
    }
    [TestMethod]
public void testAddition_CrossUnit_InchesPlusFeet()
{
    var l1 = new QuantityLength(12, LengthUnit.INCHES);
    var l2 = new QuantityLength(1, LengthUnit.FEET);

    var result = l1.Add(l2);

    Assert.AreEqual(new QuantityLength(24, LengthUnit.INCHES), result);
}

[TestMethod]
public void testAddition_SameUnit_InchesPlusInches()
{
    var l1 = new QuantityLength(10, LengthUnit.INCHES);
    var l2 = new QuantityLength(5, LengthUnit.INCHES);

    var result = l1.Add(l2);

    Assert.AreEqual(new QuantityLength(15, LengthUnit.INCHES), result);
}

[TestMethod]
public void testAddition_WithZero_ShouldReturnSameValue()
{
    var l1 = new QuantityLength(5, LengthUnit.FEET);
    var l2 = new QuantityLength(0, LengthUnit.FEET);

    var result = l1.Add(l2);

    Assert.AreEqual(new QuantityLength(5, LengthUnit.FEET), result);
}

[TestMethod]
public void testAddition_DecimalValues_FeetPlusFeet()
{
    var l1 = new QuantityLength(1.5, LengthUnit.FEET);
    var l2 = new QuantityLength(0.5, LengthUnit.FEET);

    var result = l1.Add(l2);

    Assert.AreEqual(new QuantityLength(2.0, LengthUnit.FEET), result);
}

[TestMethod]
public void testAddition_DecimalCrossUnit_ShouldConvertCorrectly()
{
    var l1 = new QuantityLength(1.5, LengthUnit.FEET); // 18 inches
    var l2 = new QuantityLength(6, LengthUnit.INCHES); // total = 24 inches = 2 feet

    var result = l1.Add(l2);

    Assert.AreEqual(new QuantityLength(2, LengthUnit.FEET), result);
}

// [TestMethod]
// [ExpectedException(typeof(ArgumentNullException))]
// public void testAddition_WhenOtherIsNull_ShouldThrowException()
// {
//     var l1 = new QuantityLength(1, LengthUnit.FEET);

//     l1.Add(null);
// }

[TestMethod]
public void testAddition_LargeValues_ShouldWorkCorrectly()
{
    var l1 = new QuantityLength(100, LengthUnit.FEET);
    var l2 = new QuantityLength(1200, LengthUnit.INCHES); // 100 feet

    var result = l1.Add(l2);

    Assert.AreEqual(new QuantityLength(200, LengthUnit.FEET), result);
}
[TestMethod]
public void testAddition_SameUnit_YardsPlusYards()
{
    var l1 = new QuantityLength(1, LengthUnit.YARDS);
    var l2 = new QuantityLength(2, LengthUnit.YARDS);

    var result = l1.Add(l2);

    Assert.AreEqual(new QuantityLength(3, LengthUnit.YARDS), result);
}

[TestMethod]
public void testAddition_CrossUnit_YardsPlusFeet()
{
    var l1 = new QuantityLength(1, LengthUnit.YARDS);  // 3 feet
    var l2 = new QuantityLength(3, LengthUnit.FEET);   // total = 6 feet = 2 yards

    var result = l1.Add(l2);

    Assert.AreEqual(new QuantityLength(2, LengthUnit.YARDS), result);
}

[TestMethod]
public void testAddition_CrossUnit_FeetPlusYards()
{
    var l1 = new QuantityLength(3, LengthUnit.FEET);   // 1 yard
    var l2 = new QuantityLength(1, LengthUnit.YARDS);  // total = 2 yards

    var result = l1.Add(l2);

    Assert.AreEqual(new QuantityLength(6, LengthUnit.FEET), result);
}

[TestMethod]
public void testAddition_CrossUnit_YardsPlusInches()
{
    var l1 = new QuantityLength(1, LengthUnit.YARDS);    // 36 inches
    var l2 = new QuantityLength(36, LengthUnit.INCHES);  // total = 72 inches = 2 yards

    var result = l1.Add(l2);

    Assert.AreEqual(new QuantityLength(2, LengthUnit.YARDS), result);
}

[TestMethod]
public void testAddition_CrossUnit_InchesPlusYards()
{
    var l1 = new QuantityLength(36, LengthUnit.INCHES);  // 1 yard
    var l2 = new QuantityLength(1, LengthUnit.YARDS);    // total = 2 yards = 72 inches

    var result = l1.Add(l2);

    Assert.AreEqual(new QuantityLength(72, LengthUnit.INCHES), result);
}

[TestMethod]
public void testAddition_CrossUnit_CmPlusCm()
{
    var l1 = new QuantityLength(100, LengthUnit.CENTIMETERS);
    var l2 = new QuantityLength(50, LengthUnit.CENTIMETERS);

    var result = l1.Add(l2);

    Assert.AreEqual(new QuantityLength(150, LengthUnit.CENTIMETERS), result);
}

[TestMethod]
public void testAddition_CrossUnit_FeetPlusCentimeters()
{
    // 1 ft = 30.48 cm, so 1 ft + 30.48 cm = 2 ft
    var l1 = new QuantityLength(1, LengthUnit.FEET);
    var l2 = new QuantityLength(30.48, LengthUnit.CENTIMETERS);

    var result = l1.Add(l2);

    Assert.AreEqual(new QuantityLength(2, LengthUnit.FEET), result);
}

[TestMethod]
public void testAddition_CrossUnit_CentimetersPlusFeet()
{
    // 30.48 cm = 1 ft, so 30.48 cm + 1 ft = 60.96 cm
    var l1 = new QuantityLength(30.48, LengthUnit.CENTIMETERS);
    var l2 = new QuantityLength(1, LengthUnit.FEET);

    var result = l1.Add(l2);

    Assert.AreEqual(new QuantityLength(60.96, LengthUnit.CENTIMETERS), result);
}

[TestMethod]
public void testAddition_CrossUnit_YardsPlusCentimeters()
{
    // 1 yard = 91.44 cm, so 1 yard + 91.44 cm = 2 yards
    var l1 = new QuantityLength(1, LengthUnit.YARDS);
    var l2 = new QuantityLength(91.44, LengthUnit.CENTIMETERS);

    var result = l1.Add(l2);

    Assert.AreEqual(new QuantityLength(2, LengthUnit.YARDS), result);
}

[TestMethod]
public void testAddition_CrossUnit_CentimetersPlusYards()
{
    // 91.44 cm = 1 yard, so 91.44 cm + 1 yard = 182.88 cm
    var l1 = new QuantityLength(91.44, LengthUnit.CENTIMETERS);
    var l2 = new QuantityLength(1, LengthUnit.YARDS);

    var result = l1.Add(l2);

    Assert.AreEqual(new QuantityLength(182.88, LengthUnit.CENTIMETERS), result);
}
    
}}