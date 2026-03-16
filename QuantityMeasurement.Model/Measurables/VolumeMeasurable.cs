using QuantityMeasurement.Model.Units;
using QuantityMeasurement.Model.Interfaces;
namespace QuantityMeasurement.Model.Measurables
{
    public readonly struct VolumeMeasurable : IMeasurable
    {   
        public VolumeUnit Unit { get; }

        public VolumeMeasurable(VolumeUnit unit) => Unit = unit;

        public double GetConversionFactor() => Unit.GetConversionFactor();
        public double ConvertToBaseUnit(double value) => Unit.ConvertToBaseUnit(value);
        public double ConvertFromBaseUnit(double baseValue) => Unit.ConvertFromBaseUnit(baseValue);
        public string GetUnitName() => Unit.ToString();
    }
}