using System;
using QuantityMeasurement.Model.Measurables;
using QuantityMeasurement.Model.Units;

namespace QuantityMeasurement.Model.Models
{
    public sealed class QuantityTemperature
    {
        private readonly Quantity<TemperatureMeasurable> _inner;

        public double Value => _inner.Value;
        public TemperatureUnit Unit => _inner.Unit.Unit;

        public QuantityTemperature(double value, TemperatureUnit unit)
        {
            _inner = new Quantity<TemperatureMeasurable>(value, new TemperatureMeasurable(unit));
        }

        public QuantityTemperature ConvertTo(TemperatureUnit targetUnit)
        {
            var converted = _inner.ConvertTo(new TemperatureMeasurable(targetUnit));
            return new QuantityTemperature(converted.Value, targetUnit);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not QuantityTemperature other)
                return false;

            return _inner.Equals(other._inner);
        }

        public override int GetHashCode() => _inner.GetHashCode();

        public override string ToString() => _inner.ToString();
    }
}
