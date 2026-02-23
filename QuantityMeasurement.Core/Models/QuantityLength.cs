using System;
using QuantityMeasurement.Core.Enums;

namespace QuantityMeasurement.Core.Models
{
    public sealed class QuantityLength
    {
        private readonly Quantity<LengthMeasurable> _inner;

        public double Value => _inner.Value;
        public LengthUnit Unit => _inner.Unit.Unit;

        public QuantityLength(double value, LengthUnit unit)
        {
            // keep your current negative message rule for length tests
            if (value < 0)
                throw new ArgumentException($"{unit} cannot be negative");

            _inner = new Quantity<LengthMeasurable>(value, new LengthMeasurable(unit));
        }

        // ------------------ CONVERSION ------------------

        public QuantityLength ConvertTo(LengthUnit targetUnit)
        {
            var converted = _inner.ConvertTo(new LengthMeasurable(targetUnit));
            return new QuantityLength(converted.Value, targetUnit);
        }

      
        public static double Convert(double value, LengthUnit source, LengthUnit target)
        {
            // uses the same logic as Quantity<> now
            var q = new Quantity<LengthMeasurable>(value, new LengthMeasurable(source));
            var converted = q.ConvertTo(new LengthMeasurable(target));
            return converted.Value;
        }

   
        public QuantityLength Add(QuantityLength other)
            => Add(other, this.Unit);

        public static QuantityLength Add(QuantityLength first, QuantityLength second)
        {
            if (first is null || second is null)
                throw new ArgumentException("Operands cannot be null");

            return first.Add(second);
        }

        public QuantityLength Add(QuantityLength other, LengthUnit targetUnit)
        {
            if (other is null)
                throw new ArgumentException("Second operand cannot be null");

            if (!Enum.IsDefined(typeof(LengthUnit), targetUnit))
                throw new ArgumentException("Invalid target unit");

            var sum = _inner.Add(other._inner, new LengthMeasurable(targetUnit));
            return new QuantityLength(sum.Value, targetUnit);
        }

        public static QuantityLength Add(QuantityLength first, QuantityLength second, LengthUnit targetUnit)
        {
            if (first is null || second is null)
                throw new ArgumentException("Operands cannot be null");

            return first.Add(second, targetUnit);
        }

        // ------------------ EQUALITY ------------------

        public override bool Equals(object? obj)
        {
            if (obj is not QuantityLength other)
                return false;

            return _inner.Equals(other._inner);
        }

        public override int GetHashCode() => _inner.GetHashCode();

        public override string ToString() => _inner.ToString();
    }
}