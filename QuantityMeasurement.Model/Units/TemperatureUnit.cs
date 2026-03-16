using System;

namespace QuantityMeasurement.Model.Units
{
    public enum TemperatureUnit
    {
        CELSIUS,
        FAHRENHEIT,
        KELVIN
    }

    public static class TemperatureUnitExtensions
    {
        // Conversion factor (not very meaningful for temperature because of offset)
        public static double GetConversionFactor(this TemperatureUnit unit)
        {
            if (unit == TemperatureUnit.CELSIUS)
                return 1.0;

            if (unit == TemperatureUnit.FAHRENHEIT)
                return 5.0 / 9.0;

            if (unit == TemperatureUnit.KELVIN)
                return 1.0;

            throw new ArgumentException("Invalid temperature unit");
        }

        // Convert to base unit (Celsius)
        public static double ConvertToBaseUnit(this TemperatureUnit unit, double value)
        {
            if (unit == TemperatureUnit.CELSIUS)
                return value;

            if (unit == TemperatureUnit.FAHRENHEIT)
                return (value - 32) * 5.0 / 9.0;

            if (unit == TemperatureUnit.KELVIN)
                return value - 273.15;

            throw new ArgumentException("Invalid temperature unit");
        }

        // Convert from base unit (Celsius) to target unit
        public static double ConvertFromBaseUnit(this TemperatureUnit unit, double baseValue)
        {
            if (unit == TemperatureUnit.CELSIUS)
                return baseValue;

            if (unit == TemperatureUnit.FAHRENHEIT)
                return (baseValue * 9.0 / 5.0) + 32;

            if (unit == TemperatureUnit.KELVIN)
                return baseValue + 273.15;

            throw new ArgumentException("Invalid temperature unit");
        }
    }
}