using System;

namespace QuantityMeasurement.Core.Enums
{
    public enum WeightUnit
    {
        KILOGRAM,
        GRAM,
        POUND
    }

    public static class WeightUnitExtensions
    {
        // Return conversion factor to convert unit into Kilogram (Base Unit)
        public static double GetConversionFactor(this WeightUnit unit)
        {
            if (unit == WeightUnit.KILOGRAM)
                return 1.0;

            if (unit == WeightUnit.GRAM)
                return 0.001;      // 1 Gram = 0.001 Kg

            if (unit == WeightUnit.POUND)
                return 0.453592;   // 1 Pound = 0.453592 Kg

            throw new ArgumentException("Invalid weight unit");
        }

        // Convert given value to base unit (Kilogram)
        public static double ConvertToBaseUnit(this WeightUnit unit, double value)
        {
            double factor = unit.GetConversionFactor();
            return value * factor;
        }

        // Convert from base unit (Kilogram) to given unit
        public static double ConvertFromBaseUnit(this WeightUnit unit, double baseValue)
        {
            double factor = unit.GetConversionFactor();
            return baseValue / factor;
        }
    }
}