using System;

namespace QuantityMeasurement.Model.Units
{
    public enum VolumeUnit
    {
        LITRE,
        MILLILITRE,
        GALLON
    }

    public static class VolumeUnitExtensions
    {
        // factor to convert FROM this unit TO base (LITRE)
        public static double GetConversionFactor(this VolumeUnit unit) => unit switch
        {
            VolumeUnit.LITRE => 1.0,
            VolumeUnit.MILLILITRE => 0.001,   // 1 mL = 0.001 L
            VolumeUnit.GALLON => 3.78541,     // 1 gal = 3.78541 L (US gallon)
            _ => throw new ArgumentException("Invalid volume unit")
        };

        public static double ConvertToBaseUnit(this VolumeUnit unit, double value)
            => value * unit.GetConversionFactor();

        public static double ConvertFromBaseUnit(this VolumeUnit unit, double baseValue)
            => baseValue / unit.GetConversionFactor();
    }
}