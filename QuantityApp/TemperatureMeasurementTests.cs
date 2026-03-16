
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurement.Model.Units;
using QuantityMeasurement.Model.Models;
using QuantityMeasurement.Business.Impl;
using System;

namespace QuantityMeasurement.Tests
{
    [TestClass]
    public class TemperatureMeasurementTests
    {
        private QuantityMeasurementServiceImpl service;

        [TestInitialize]
        public void Setup()
        {
            service = new QuantityMeasurementServiceImpl();
        }

        // ---------------- TEMPERATURE EQUALITY ----------------

        [TestMethod]
        public void TemperatureEquality_CelsiusToCelsius_SameValue()
        {
            bool result = service.AreTemperaturesEqual(0.0, TemperatureUnit.CELSIUS, 0.0, TemperatureUnit.CELSIUS);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TemperatureEquality_FahrenheitToFahrenheit_SameValue()
        {
            bool result = service.AreTemperaturesEqual(32.0, TemperatureUnit.FAHRENHEIT, 32.0, TemperatureUnit.FAHRENHEIT);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TemperatureEquality_CelsiusToFahrenheit_0C_Equals_32F()
        {
            bool result = service.AreTemperaturesEqual(0.0, TemperatureUnit.CELSIUS, 32.0, TemperatureUnit.FAHRENHEIT);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TemperatureEquality_CelsiusToFahrenheit_100C_Equals_212F()
        {
            bool result = service.AreTemperaturesEqual(100.0, TemperatureUnit.CELSIUS, 212.0, TemperatureUnit.FAHRENHEIT);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TemperatureEquality_Negative40_IsSame()
        {
            bool result = service.AreTemperaturesEqual(-40.0, TemperatureUnit.CELSIUS, -40.0, TemperatureUnit.FAHRENHEIT);
            Assert.IsTrue(result);
        }

        // ---------------- TEMPERATURE CONVERSION ----------------

        [TestMethod]
        public void TemperatureConversion_CelsiusToFahrenheit()
        {
            var q = new QuantityMeasurement.Model.DTO.QuantityDTO(100.0, "CELSIUS");
            var result = service.Convert(q, "FAHRENHEIT");

            Assert.AreEqual(212.0, result.Value, 1e-6);
        }

        [TestMethod]
        public void TemperatureConversion_FahrenheitToCelsius()
        {
            var q = new QuantityMeasurement.Model.DTO.QuantityDTO(32.0, "FAHRENHEIT");
            var result = service.Convert(q, "CELSIUS");

            Assert.AreEqual(0.0, result.Value, 1e-6);
        }
    }
}
