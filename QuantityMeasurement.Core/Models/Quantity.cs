using System;
using QuantityMeasurement.Core.Interfaces;

namespace QuantityMeasurement.Core.Models
{
    public sealed class Quantity<U> where U : struct, IMeasurable
    {
        private const double Epsilon = 1e-6;

        public double Value { get; }
        public U Unit { get; }

        public Quantity(double value, U unit)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Value must be a finite number");

            Value = value;
            Unit = unit;
        }

        private double ToBase() => Unit.ConvertToBaseUnit(Value);

        public Quantity<U> ConvertTo(U targetUnit)
        {
            double baseValue = ToBase();
            double targetValue = targetUnit.ConvertFromBaseUnit(baseValue);

            return new Quantity<U>(targetValue, targetUnit);
        }

        public Quantity<U> Add(Quantity<U> other) => Add(other, this.Unit);

        public Quantity<U> Add(Quantity<U> other, U targetUnit)
        {
            if (other is null) throw new ArgumentNullException(nameof(other));

            double sumBase = this.ToBase() + other.ToBase();
            double targetValue = targetUnit.ConvertFromBaseUnit(sumBase);

            return new Quantity<U>(targetValue, targetUnit);
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj is not Quantity<U> other) return false;

            return Math.Abs(this.ToBase() - other.ToBase()) <= Epsilon;
        }

        public override int GetHashCode()
        {
            // stable hash based on base value bucket
            double baseValue = ToBase();
            long bucket = (long)Math.Round(baseValue / Epsilon);
            return HashCode.Combine(bucket, Unit.GetType());
        }

        public override string ToString() => $"Quantity({Value}, {Unit.GetUnitName()})";
    }
}