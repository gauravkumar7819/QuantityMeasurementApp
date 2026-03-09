
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurement.Core.Enums;
using QuantityMeasurement.Core.Models;
using System;

namespace QuantityMeasurement.Tests
{
    [TestClass]
    public class TemperatureMeasurementTests
    {
        private const double Eps = 1e-6;

        // ---------------- TEMPERATURE EQUALITY ----------------

        [TestMethod]
        public void TemperatureEquality_CelsiusToCelsius_SameValue()
        {
            var t1 = new Quantity<TemperatureMeasurable>(0.0, new TemperatureMeasurable(TemperatureUnit.CELSIUS));
            var t2 = new Quantity<TemperatureMeasurable>(0.0, new TemperatureMeasurable(TemperatureUnit.CELSIUS));

            Assert.IsTrue(t1.Equals(t2));
        }

        [TestMethod]
        public void TemperatureEquality_FahrenheitToFahrenheit_SameValue()
        {
            var t1 = new Quantity<TemperatureMeasurable>(32.0, new TemperatureMeasurable(TemperatureUnit.FAHRENHEIT));
            var t2 = new Quantity<TemperatureMeasurable>(32.0, new TemperatureMeasurable(TemperatureUnit.FAHRENHEIT));

            Assert.IsTrue(t1.Equals(t2));
        }

        [TestMethod]
        public void TemperatureEquality_CelsiusToFahrenheit_0C_Equals_32F()
        {
            var c = new Quantity<TemperatureMeasurable>(0.0, new TemperatureMeasurable(TemperatureUnit.CELSIUS));
            var f = new Quantity<TemperatureMeasurable>(32.0, new TemperatureMeasurable(TemperatureUnit.FAHRENHEIT));

            Assert.IsTrue(c.Equals(f));
        }

        [TestMethod]
        public void TemperatureEquality_CelsiusToFahrenheit_100C_Equals_212F()
        {
            var c = new Quantity<TemperatureMeasurable>(100.0, new TemperatureMeasurable(TemperatureUnit.CELSIUS));
            var f = new Quantity<TemperatureMeasurable>(212.0, new TemperatureMeasurable(TemperatureUnit.FAHRENHEIT));

            Assert.IsTrue(c.Equals(f));
        }

        [TestMethod]
        public void TemperatureEquality_Negative40_IsSame()
        {
            var c = new Quantity<TemperatureMeasurable>(-40.0, new TemperatureMeasurable(TemperatureUnit.CELSIUS));
            var f = new Quantity<TemperatureMeasurable>(-40.0, new TemperatureMeasurable(TemperatureUnit.FAHRENHEIT));

            Assert.IsTrue(c.Equals(f));
        }

        // ---------------- TEMPERATURE CONVERSION ----------------

        [TestMethod]
        public void TemperatureConversion_CelsiusToFahrenheit()
        {
            var c = new Quantity<TemperatureMeasurable>(100.0, new TemperatureMeasurable(TemperatureUnit.CELSIUS));

            var result = c.ConvertTo(new TemperatureMeasurable(TemperatureUnit.FAHRENHEIT));

            Assert.AreEqual(212.0, result.Value, Eps);
        }

        [TestMethod]
        public void TemperatureConversion_FahrenheitToCelsius()
        {
            var f = new Quantity<TemperatureMeasurable>(32.0, new TemperatureMeasurable(TemperatureUnit.FAHRENHEIT));

            var result = f.ConvertTo(new TemperatureMeasurable(TemperatureUnit.CELSIUS));

            Assert.AreEqual(0.0, result.Value, Eps);
        }

        [TestMethod]
        public void TemperatureConversion_SameUnit()
        {
            var c = new Quantity<TemperatureMeasurable>(50.0, new TemperatureMeasurable(TemperatureUnit.CELSIUS));

            var result = c.ConvertTo(new TemperatureMeasurable(TemperatureUnit.CELSIUS));

            Assert.AreEqual(50.0, result.Value, Eps);
        }

        [TestMethod]
        public void TemperatureConversion_RoundTrip()
        {
            var c = new Quantity<TemperatureMeasurable>(25.0, new TemperatureMeasurable(TemperatureUnit.CELSIUS));

            var f = c.ConvertTo(new TemperatureMeasurable(TemperatureUnit.FAHRENHEIT));
            var back = f.ConvertTo(new TemperatureMeasurable(TemperatureUnit.CELSIUS));

            Assert.AreEqual(25.0, back.Value, Eps);
        }

        // ---------------- EDGE CASES ----------------

        [TestMethod]
        public void Temperature_AbsoluteZero()
        {
            var c = new Quantity<TemperatureMeasurable>(-273.15, new TemperatureMeasurable(TemperatureUnit.CELSIUS));

            var k = c.ConvertTo(new TemperatureMeasurable(TemperatureUnit.KELVIN));

            Assert.AreEqual(0.0, k.Value, Eps);
        }

        [TestMethod]
        public void Temperature_NegativeValues()
        {
            var c = new Quantity<TemperatureMeasurable>(-20.0, new TemperatureMeasurable(TemperatureUnit.CELSIUS));

            var f = c.ConvertTo(new TemperatureMeasurable(TemperatureUnit.FAHRENHEIT));

            Assert.AreEqual(-4.0, f.Value, Eps);
        }



     
  
    }
}

