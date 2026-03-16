using System;
using QuantityMeasurement.Model.Units;
using QuantityMeasurement.Model.Measurables;
namespace QuantityMeasurement.Model.Models
{
    public sealed class QuantityWeight
    {
        private readonly Quantity<WeightMeasurable> _inner;

        public double Value => _inner.Value;
        public WeightUnit Unit => _inner.Unit.Unit;

        public QuantityWeight(double value, WeightUnit unit)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Value must be a finite number");

            _inner = new Quantity<WeightMeasurable>(value, new WeightMeasurable(unit));
        }

   
        public QuantityWeight ConvertTo(WeightUnit targetUnit)
        {
            var converted = _inner.ConvertTo(new WeightMeasurable(targetUnit));
            return new QuantityWeight(converted.Value, targetUnit);
        }

   
        public QuantityWeight Add(QuantityWeight other)
            => Add(other, this.Unit);

      
        public QuantityWeight Add(QuantityWeight other, WeightUnit targetUnit)
        {
            if (other is null) throw new ArgumentNullException(nameof(other));

            if (!Enum.IsDefined(typeof(WeightUnit), targetUnit))
                throw new ArgumentException("Invalid target unit");

            var sum = _inner.Add(other._inner, new WeightMeasurable(targetUnit));
            return new QuantityWeight(sum.Value, targetUnit);
        }

        public QuantityWeight Subtract(QuantityWeight other, WeightUnit targetUnit)
        {
            if (other is null) throw new ArgumentNullException(nameof(other));

            var diff = _inner.Subtract(other._inner, new WeightMeasurable(targetUnit));
            return new QuantityWeight(diff.Value, targetUnit);
        }


        public override bool Equals(object? obj)
        {
            if (obj is not QuantityWeight other)
                return false;

            return _inner.Equals(other._inner);
        }

        public override int GetHashCode() => _inner.GetHashCode();

        public override string ToString() => _inner.ToString();
    }
}