using System;
using QuantityMeasurement.Model.Units;
using QuantityMeasurement.Model.Measurable;
namespace QuantityMeasurement.Model.Models
{
    public sealed class QuantityWeight
    {
        private readonly Quantity<WeightMeasurable> _inner;

        public double Value => _inner.Value;
        public WeightUnit Unit => _inner.Unit.Unit;

        public QuantityWeight(double value, WeightUnit unit)
        {
            _inner = new Quantity<WeightMeasurable>(value, new WeightMeasurable(unit));
        }

        public QuantityWeight ConvertTo(WeightUnit targetUnit)
        {
            var converted = _inner.ConvertTo(new WeightMeasurable(targetUnit));
            return new QuantityWeight(converted.Value, targetUnit);
        }

        public QuantityWeight Add(QuantityWeight other, WeightUnit targetUnit)
        {
            if (other is null)
                throw new ArgumentException("Second operand cannot be null");

            var sum = _inner.Add(other._inner, new WeightMeasurable(targetUnit));
            return new QuantityWeight(sum.Value, targetUnit);
        }

        public QuantityWeight Subtract(QuantityWeight other, WeightUnit targetUnit)
        {
            if (other is null)
                throw new ArgumentException("Second operand cannot be null");

            var diff = _inner.Subtract(other._inner, new WeightMeasurable(targetUnit));
            return new QuantityWeight(diff.Value, targetUnit);
        }

        public override bool Equals(object? obj) => obj is QuantityWeight other && _inner.Equals(other._inner);
        public override int GetHashCode() => _inner.GetHashCode();
        public override string ToString() => _inner.ToString();
    }
}