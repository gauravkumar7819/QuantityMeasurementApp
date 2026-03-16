using QuantityMeasurement.Model.Units;
using QuantityMeasurement.Model.Interfaces;

namespace QuantityMeasurement.Model.Measurables
{
    public struct TemperatureMeasurable : IMeasurable
    { 
        public TemperatureUnit Unit { get; }

        public TemperatureMeasurable(TemperatureUnit unit)
        {
            Unit = unit;
        }

        public double GetConversionFactor()
        {
            return 1; // not used for temperature
        }

        public double ConvertToBaseUnit(double value)
        {
            switch (Unit)
            {
                case TemperatureUnit.CELSIUS:
                    return value;

                case TemperatureUnit.FAHRENHEIT:
                    return (value - 32) * 5.0 / 9.0;

                case TemperatureUnit.KELVIN:
                    return value - 273.15;

                default:
                    throw new NotSupportedException();
            }
        }

        public double ConvertFromBaseUnit(double baseValue)
        {
            switch (Unit)
            {
                case TemperatureUnit.CELSIUS:
                    return baseValue;

                case TemperatureUnit.FAHRENHEIT:
                    return (baseValue * 9.0 / 5.0) + 32;

                case TemperatureUnit.KELVIN:
                    return baseValue + 273.15;

                default:
                    throw new NotSupportedException();
            }
        }

        public string GetUnitName()
        {
            return Unit.ToString();
        }
    }
}