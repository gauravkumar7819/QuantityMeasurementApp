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

        // ------------------ CONVERT ------------------

        public Quantity<U> ConvertTo(U targetUnit)
        {
            double baseValue = ToBase();
            double targetValue = targetUnit.ConvertFromBaseUnit(baseValue);
            return new Quantity<U>(targetValue, targetUnit);
        }

        // ------------------ ADD ------------------

        public Quantity<U> Add(Quantity<U> other) => Add(other, this.Unit);

        public Quantity<U> Add(Quantity<U> other, U targetUnit)
        {
            if (other is null) throw new ArgumentNullException(nameof(other));

            double sumBase = this.ToBase() + other.ToBase();
            double targetValue = targetUnit.ConvertFromBaseUnit(sumBase);

            return new Quantity<U>(targetValue, targetUnit);
        }

        // ------------------ UC12: SUBTRACT ------------------

        public Quantity<U> Subtract(Quantity<U> other) => Subtract(other, this.Unit);

        public Quantity<U> Subtract(Quantity<U> other, U targetUnit)
        {
            if (other is null) throw new ArgumentNullException(nameof(other));

            double diffBase = this.ToBase() - other.ToBase();
            double targetValue = targetUnit.ConvertFromBaseUnit(diffBase);

            return new Quantity<U>(targetValue, targetUnit);
        }



        public double Divide(Quantity<U> other)
        {
            if (other is null) throw new ArgumentNullException(nameof(other));

            double divisorBase = other.ToBase();
            if (Math.Abs(divisorBase) <= Epsilon)
                throw new DivideByZeroException("Cannot divide by a zero quantity");

            double dividendBase = this.ToBase();
            return dividendBase / divisorBase;
        }

        // ------------------ EQUALITY ------------------

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj is not Quantity<U> other) return false;

            return Math.Abs(this.ToBase() - other.ToBase()) <= Epsilon;
        }

        public override int GetHashCode()
        {
            double baseValue = ToBase();
            long bucket = (long)Math.Round(baseValue / Epsilon);
            return HashCode.Combine(bucket, Unit.GetType());
        }
        public override string ToString() => $"Quantity({Value}, {Unit.GetUnitName()})";
    }
}