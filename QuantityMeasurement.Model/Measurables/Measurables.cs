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
        public void Validate(double value) { if (value < 0) throw new System.ArgumentException($"{Unit} cannot be negative"); }
    }

    public readonly struct WeightMeasurable : IMeasurable
    {
        public WeightUnit Unit { get; }
        public WeightMeasurable(WeightUnit unit) => Unit = unit;
        public double GetConversionFactor() => Unit.GetConversionFactor();
        public double ConvertToBaseUnit(double value) => Unit.ConvertToBaseUnit(value);
        public double ConvertFromBaseUnit(double baseValue) => Unit.ConvertFromBaseUnit(baseValue);
        public string GetUnitName() => Unit.ToString();
        public void Validate(double value) { if (value < 0) throw new System.ArgumentException($"{Unit} cannot be negative"); }
    }

    public readonly struct VolumeMeasurable : IMeasurable
    {
        public VolumeUnit Unit { get; }
        public VolumeMeasurable(VolumeUnit unit) => Unit = unit;
        public double GetConversionFactor() => Unit.GetConversionFactor();
        public double ConvertToBaseUnit(double value) => Unit.ConvertToBaseUnit(value);
        public double ConvertFromBaseUnit(double baseValue) => Unit.ConvertFromBaseUnit(baseValue);
        public string GetUnitName() => Unit.ToString();
        public void Validate(double value) { if (value < 0) throw new System.ArgumentException($"{Unit} cannot be negative"); }
    }

    public readonly struct TemperatureMeasurable : IMeasurable
    {
        public TemperatureUnit Unit { get; }
        public TemperatureMeasurable(TemperatureUnit unit) => Unit = unit;
        public double GetConversionFactor() => Unit.GetConversionFactor();
        public double ConvertToBaseUnit(double value) => Unit.ConvertToBaseUnit(value);
        public double ConvertFromBaseUnit(double baseValue) => Unit.ConvertFromBaseUnit(baseValue);
        public string GetUnitName() => Unit.ToString();
        public void Validate(double value) { } // Temperature can be negative
    }
}
