namespace Shared.Models.Units
{
    public enum LengthUnit
    {
        INCHES,
        FEET,
        YARDS,
        CENTIMETERS,
        METERS,
        KILOMETERS
    }

    public static class LengthUnitExtensions
    {
        // Base Unit = METERS

        public static double GetConversionFactor(this LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.METERS => 1.0,
                LengthUnit.CENTIMETERS => 0.01,
                LengthUnit.FEET => 0.3048,
                LengthUnit.INCHES => 0.0254,
                LengthUnit.YARDS => 0.9144,
                LengthUnit.KILOMETERS => 1000.0,
                _ => throw new ArgumentException("Invalid LengthUnit")
            };
        }

        public static double ConvertToBaseUnit(this LengthUnit unit, double value)
        {
            return value * unit.GetConversionFactor();
        }

        public static double ConvertFromBaseUnit(this LengthUnit unit, double baseValue)
        {
            return baseValue / unit.GetConversionFactor();
        }
    }
}
