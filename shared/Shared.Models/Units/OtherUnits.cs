namespace Shared.Models.Units
{
    public enum WeightUnit
    {
        GRAMS,
        KILOGRAMS,
        POUNDS,
        OUNCES
    }

    public static class WeightUnitExtensions
    {
        // Base Unit = KILOGRAMS

        public static double GetConversionFactor(this WeightUnit unit)
        {
            return unit switch
            {
                WeightUnit.KILOGRAMS => 1.0,
                WeightUnit.GRAMS => 0.001,
                WeightUnit.POUNDS => 0.453592,
                WeightUnit.OUNCES => 0.0283495,
                _ => throw new ArgumentException("Invalid WeightUnit")
            };
        }

        public static double ConvertToBaseUnit(this WeightUnit unit, double value)
        {
            return value * unit.GetConversionFactor();
        }

        public static double ConvertFromBaseUnit(this WeightUnit unit, double baseValue)
        {
            return baseValue / unit.GetConversionFactor();
        }
    }

    public enum VolumeUnit
    {
        LITERS,
        MILLILITERS,
        GALLONS,
        CUBIC_METERS
    }

    public static class VolumeUnitExtensions
    {
        // Base Unit = LITERS

        public static double GetConversionFactor(this VolumeUnit unit)
        {
            return unit switch
            {
                VolumeUnit.LITERS => 1.0,
                VolumeUnit.MILLILITERS => 0.001,
                VolumeUnit.GALLONS => 3.78541,
                VolumeUnit.CUBIC_METERS => 1000.0,
                _ => throw new ArgumentException("Invalid VolumeUnit")
            };
        }

        public static double ConvertToBaseUnit(this VolumeUnit unit, double value)
        {
            return value * unit.GetConversionFactor();
        }

        public static double ConvertFromBaseUnit(this VolumeUnit unit, double baseValue)
        {
            return baseValue / unit.GetConversionFactor();
        }
    }

    public enum TemperatureUnit
    {
        CELSIUS,
        FAHRENHEIT,
        KELVIN
    }

    public static class TemperatureUnitExtensions
    {
        // Base Unit = KELVIN

        public static double GetConversionFactor(this TemperatureUnit unit)
        {
            return unit switch
            {
                TemperatureUnit.KELVIN => 1.0,
                TemperatureUnit.CELSIUS => 1.0, // Special handling in conversion
                TemperatureUnit.FAHRENHEIT => 1.0, // Special handling in conversion
                _ => throw new ArgumentException("Invalid TemperatureUnit")
            };
        }

        public static double ConvertToBaseUnit(this TemperatureUnit unit, double value)
        {
            return unit switch
            {
                TemperatureUnit.KELVIN => value,
                TemperatureUnit.CELSIUS => value + 273.15,
                TemperatureUnit.FAHRENHEIT => (value - 32) * 5 / 9 + 273.15,
                _ => throw new ArgumentException("Invalid TemperatureUnit")
            };
        }

        public static double ConvertFromBaseUnit(this TemperatureUnit unit, double baseValue)
        {
            return unit switch
            {
                TemperatureUnit.KELVIN => baseValue,
                TemperatureUnit.CELSIUS => baseValue - 273.15,
                TemperatureUnit.FAHRENHEIT => (baseValue - 273.15) * 9 / 5 + 32,
                _ => throw new ArgumentException("Invalid TemperatureUnit")
            };
        }
    }
}
