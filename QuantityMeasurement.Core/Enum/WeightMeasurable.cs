using QuantityMeasurement.Core.Interfaces;

namespace QuantityMeasurement.Core.Enums
{
    public readonly struct WeightMeasurable : IMeasurable
    {
        public WeightUnit Unit { get; }

        public WeightMeasurable(WeightUnit unit) => Unit = unit;

        public double GetConversionFactor() => Unit.GetConversionFactor();
        public double ConvertToBaseUnit(double value) => Unit.ConvertToBaseUnit(value);
        public double ConvertFromBaseUnit(double baseValue) => Unit.ConvertFromBaseUnit(baseValue);
        public string GetUnitName() => Unit.ToString();
    }
}