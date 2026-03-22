using System;
using QuantityMeasurement.Model.Measurable;
using QuantityMeasurement.Model.Units;

namespace QuantityMeasurement.Model.Models
{
    public sealed class QuantityLength
    {
        private readonly Quantity<LengthMeasurable> _inner;

        public double Value => _inner.Value;
        public LengthUnit Unit => _inner.Unit.Unit;

        public QuantityLength(double value, LengthUnit unit)
        {
            _inner = new Quantity<LengthMeasurable>(value, new LengthMeasurable(unit));
        }

        public QuantityLength ConvertTo(LengthUnit targetUnit)
        {
            var converted = _inner.ConvertTo(new LengthMeasurable(targetUnit));
            return new QuantityLength(converted.Value, targetUnit);
        }

        public QuantityLength Add(QuantityLength other, LengthUnit targetUnit)
        {
            if (other is null)
                throw new ArgumentException("Second operand cannot be null");

            var sum = _inner.Add(other._inner, new LengthMeasurable(targetUnit));
            return new QuantityLength(sum.Value, targetUnit);
        }

        public QuantityLength Subtract(QuantityLength other, LengthUnit targetUnit)
        {
            if (other is null)
                throw new ArgumentException("Second operand cannot be null");

            var diff = _inner.Subtract(other._inner, new LengthMeasurable(targetUnit));
            return new QuantityLength(diff.Value, targetUnit);
        }

        public override bool Equals(object? obj) => obj is QuantityLength other && _inner.Equals(other._inner);
        public override int GetHashCode() => _inner.GetHashCode();
        public override string ToString() => _inner.ToString();
    }
}