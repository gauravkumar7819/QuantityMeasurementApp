namespace QuantityMeasurement.Model.Units{

    public enum LengthUnit
    {
        INCHES,
        FEET,
        YARDS,
        CENTIMETERS
    }

    public static class LengthUnitExtensions
    {
        // Base Unit = FEET

        public static double GetConversionFactor(this LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.FEET => 1.0,              // Base unit
                LengthUnit.INCHES => 1.0 / 12.0,     // 1 inch = 1/12 feet
                LengthUnit.YARDS => 3.0,             // 1 yard = 3 feet
                LengthUnit.CENTIMETERS => 1.0 / 30.48, // 1 cm = 1/30.48 feet
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