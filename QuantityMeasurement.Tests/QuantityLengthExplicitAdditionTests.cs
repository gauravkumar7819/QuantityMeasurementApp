using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurement.Core;
using QuantityMeasurement.Core.Enums;
using QuantityMeasurement.Core.Models;
using QuantityMeasurement.Core.Service;
using System;

namespace QuantityMeasurement.Tests
{
[TestClass]
public class QuantityLengthExplicitAdditionTests
{
    [TestMethod]
    public void testAddition_ExplicitTargetUnit_Feet()
    {
        var l1 = new QuantityLength(1, LengthUnit.FEET);
        var l2 = new QuantityLength(12, LengthUnit.INCHES);

        var result = l1.Add(l2, LengthUnit.FEET);

        Assert.AreEqual(new QuantityLength(2, LengthUnit.FEET), result);
    }

    [TestMethod]
    public void testAddition_ExplicitTargetUnit_Inches()
    {
        var l1 = new QuantityLength(1, LengthUnit.FEET);
        var l2 = new QuantityLength(12, LengthUnit.INCHES);

        var result = l1.Add(l2, LengthUnit.INCHES);

        Assert.AreEqual(new QuantityLength(24, LengthUnit.INCHES), result);
    }

    [TestMethod]
    public void testAddition_ExplicitTargetUnit_Yards()
    {
        var l1 = new QuantityLength(1, LengthUnit.FEET);
        var l2 = new QuantityLength(12, LengthUnit.INCHES);

        var result = l1.Add(l2, LengthUnit.YARDS);

        Assert.AreEqual(0.666666, result.Value, 1e-5);
    }
    [TestMethod]
public void testAddition_ExplicitTargetUnit_SameUnit_Feet()
{
    var l1 = new QuantityLength(2, LengthUnit.FEET);
    var l2 = new QuantityLength(3, LengthUnit.FEET);

    var result = l1.Add(l2, LengthUnit.FEET);

    Assert.AreEqual(new QuantityLength(5, LengthUnit.FEET), result);
}

[TestMethod]
public void testAddition_ExplicitTargetUnit_SameUnit_Inches()
{
    var l1 = new QuantityLength(10, LengthUnit.INCHES);
    var l2 = new QuantityLength(5, LengthUnit.INCHES);

    var result = l1.Add(l2, LengthUnit.INCHES);

    Assert.AreEqual(new QuantityLength(15, LengthUnit.INCHES), result);
}

[TestMethod]
public void testAddition_ExplicitTargetUnit_SameUnit_Yards()
{
    var l1 = new QuantityLength(1, LengthUnit.YARDS);
    var l2 = new QuantityLength(2, LengthUnit.YARDS);

    var result = l1.Add(l2, LengthUnit.YARDS);

    Assert.AreEqual(new QuantityLength(3, LengthUnit.YARDS), result);
}

[TestMethod]
public void testAddition_ExplicitTargetUnit_Inches_FromYardsPlusFeet()
{
    var l1 = new QuantityLength(1, LengthUnit.YARDS); // 36 in
    var l2 = new QuantityLength(1, LengthUnit.FEET);  // 12 in => total 48 in

    var result = l1.Add(l2, LengthUnit.INCHES);

    Assert.AreEqual(new QuantityLength(48, LengthUnit.INCHES), result);
}

[TestMethod]
public void testAddition_ExplicitTargetUnit_Feet_FromYardsPlusInches()
{
    var l1 = new QuantityLength(1, LengthUnit.YARDS);   // 3 ft
    var l2 = new QuantityLength(12, LengthUnit.INCHES); // 1 ft => total 4 ft

    var result = l1.Add(l2, LengthUnit.FEET);

    Assert.AreEqual(new QuantityLength(4, LengthUnit.FEET), result);
}

[TestMethod]
public void testAddition_ExplicitTargetUnit_Yards_FromFeetPlusFeet()
{
    var l1 = new QuantityLength(3, LengthUnit.FEET); // 1 yard
    var l2 = new QuantityLength(3, LengthUnit.FEET); // total 2 yards

    var result = l1.Add(l2, LengthUnit.YARDS);

    Assert.AreEqual(new QuantityLength(2, LengthUnit.YARDS), result);
}

[TestMethod]
public void testAddition_ExplicitTargetUnit_Yards_FromInchesPlusInches()
{
    var l1 = new QuantityLength(36, LengthUnit.INCHES); // 1 yard
    var l2 = new QuantityLength(36, LengthUnit.INCHES); // total 2 yards

    var result = l1.Add(l2, LengthUnit.YARDS);

    Assert.AreEqual(new QuantityLength(2, LengthUnit.YARDS), result);
}

[TestMethod]
public void testAddition_ExplicitTargetUnit_Inches_WithZero()
{
    var l1 = new QuantityLength(0, LengthUnit.INCHES);
    var l2 = new QuantityLength(2, LengthUnit.FEET); // 24 in

    var result = l1.Add(l2, LengthUnit.INCHES);

    Assert.AreEqual(new QuantityLength(24, LengthUnit.INCHES), result);
}

[TestMethod]
public void testAddition_ExplicitTargetUnit_Feet_DecimalInput()
{
    // 1.5 ft (18 in) + 6 in = 24 in = 2 ft
    var l1 = new QuantityLength(1.5, LengthUnit.FEET);
    var l2 = new QuantityLength(6, LengthUnit.INCHES);

    var result = l1.Add(l2, LengthUnit.FEET);

    Assert.AreEqual(2.0, result.Value, 1e-9);
    Assert.AreEqual(LengthUnit.FEET, result.Unit);
}

[TestMethod]
public void testAddition_ExplicitTargetUnit_Centimeters_FromCmPlusCm()
{
    var l1 = new QuantityLength(100, LengthUnit.CENTIMETERS);
    var l2 = new QuantityLength(50, LengthUnit.CENTIMETERS);

    var result = l1.Add(l2, LengthUnit.CENTIMETERS);

    Assert.AreEqual(150, result.Value, 1e-9);
    Assert.AreEqual(LengthUnit.CENTIMETERS, result.Unit);
}

[TestMethod]
public void testAddition_ExplicitTargetUnit_Centimeters_FromFeetPlusInches()
{
    // 1 ft + 12 in = 24 in = 60.96 cm
    var l1 = new QuantityLength(1, LengthUnit.FEET);
    var l2 = new QuantityLength(12, LengthUnit.INCHES);

    var result = l1.Add(l2, LengthUnit.CENTIMETERS);

    Assert.AreEqual(60.96, result.Value, 1e-6);
    Assert.AreEqual(LengthUnit.CENTIMETERS, result.Unit);
}

[TestMethod]
public void testAddition_ExplicitTargetUnit_Feet_FromCentimetersPlusCentimeters()
{
    // 30.48 cm + 30.48 cm = 60.96 cm = 2 ft
    var l1 = new QuantityLength(30.48, LengthUnit.CENTIMETERS);
    var l2 = new QuantityLength(30.48, LengthUnit.CENTIMETERS);

    var result = l1.Add(l2, LengthUnit.FEET);

    Assert.AreEqual(2.0, result.Value, 1e-6);
    Assert.AreEqual(LengthUnit.FEET, result.Unit);
}




}}