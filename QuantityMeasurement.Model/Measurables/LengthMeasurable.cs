using QuantityMeasurement.Model.Interfaces;
using QuantityMeasurement.Model.Units;

namespace QuantityMeasurement.Model.Measurable
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