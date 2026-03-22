using System;
using QuantityMeasurement.Model.Measurable;
using QuantityMeasurement.Model.Units;

namespace QuantityMeasurement.Model.Models
{
    public sealed class QuantityVolume
    {
        private readonly Quantity<VolumeMeasurable> _inner;

        public double Value => _inner.Value;
        public VolumeUnit Unit => _inner.Unit.Unit;

        public QuantityVolume(double value, VolumeUnit unit)
        {
            _inner = new Quantity<VolumeMeasurable>(value, new VolumeMeasurable(unit));
        }

        public QuantityVolume ConvertTo(VolumeUnit targetUnit)
        {
            var converted = _inner.ConvertTo(new VolumeMeasurable(targetUnit));
            return new QuantityVolume(converted.Value, targetUnit);
        }

        public QuantityVolume Add(QuantityVolume other, VolumeUnit targetUnit)
        {
            if (other is null)
                throw new ArgumentException("Second operand cannot be null");

            var sum = _inner.Add(other._inner, new VolumeMeasurable(targetUnit));
            return new QuantityVolume(sum.Value, targetUnit);
        }

        public QuantityVolume Subtract(QuantityVolume other, VolumeUnit targetUnit)
        {
            if (other is null)
                throw new ArgumentException("Second operand cannot be null");

            var diff = _inner.Subtract(other._inner, new VolumeMeasurable(targetUnit));
            return new QuantityVolume(diff.Value, targetUnit);
        }

        public override bool Equals(object? obj) => obj is QuantityVolume other && _inner.Equals(other._inner);
        public override int GetHashCode() => _inner.GetHashCode();
        public override string ToString() => _inner.ToString();
    }
}
