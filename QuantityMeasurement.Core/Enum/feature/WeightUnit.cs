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
        // Factor to convert FROM this unit TO base (kilogram)
        public static double ToKilogramFactor(this WeightUnit unit) => unit switch
        {
            WeightUnit.KILOGRAM => 1.0,
            WeightUnit.GRAM => 0.001,        // 1 g = 0.001 kg
            WeightUnit.POUND => 0.453592,    // 1 lb = 0.453592 kg
            _ => throw new ArgumentException("Invalid weight unit")
        };

        public static double ConvertToBaseUnit(this WeightUnit unit, double valueInUnit)
        { 
            return valueInUnit * unit.ToKilogramFactor();
        }

        public static double ConvertFromBaseUnit(this WeightUnit unit, double valueInKilograms)
        {
            return valueInKilograms / unit.ToKilogramFactor();
        }
    }
}