using QuantityMeasurement.Core.Interfaces;

namespace QuantityMeasurement.Core.Enums
{
    public readonly struct LengthMeasurable : IMeasurable
    {
        public LengthUnit Unit { get; }

        public LengthMeasurable(LengthUnit unit) => Unit = unit;

        public double GetConversionFactor() => Unit.GetConversionFactor();
        public double ConvertToBaseUnit(double value) => Unit.ConvertToBaseUnit(value);
        public double ConvertFromBaseUnit(double baseValue) => Unit.ConvertFromBaseUnit(baseValue);
        public string GetUnitName() => Unit.ToString();
    }
}