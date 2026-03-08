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

        // =========================
        // UC13 REFACTOR
        // =========================

        private enum ArithmeticOperation
        {
            Add,
            Subtract,
            Divide
        }

        private void ValidateArithmeticOperands(Quantity<U> other)
        {
            if (other is null)
                throw new ArgumentNullException(nameof(other));
        }

        private double PerformBaseArithmetic(Quantity<U> other, ArithmeticOperation operation)
        {
            ValidateArithmeticOperands(other);

            double leftBase = this.ToBase();
            double rightBase = other.ToBase();

            return operation switch
            {
                ArithmeticOperation.Add => leftBase + rightBase,

                ArithmeticOperation.Subtract => leftBase - rightBase,

                ArithmeticOperation.Divide =>
                    Math.Abs(rightBase) <= Epsilon
                        ? throw new DivideByZeroException("Cannot divide by zero quantity")
                        : leftBase / rightBase,

                _ => throw new InvalidOperationException("Unsupported operation")
            };
        }

        // ------------------ ADD ------------------

        public Quantity<U> Add(Quantity<U> other)
        {
            return Add(other, this.Unit);
        }

        public Quantity<U> Add(Quantity<U> other, U targetUnit)
        {
            double baseResult = PerformBaseArithmetic(other, ArithmeticOperation.Add);
            double targetValue = targetUnit.ConvertFromBaseUnit(baseResult);

            return new Quantity<U>(targetValue, targetUnit);
        }

        // ------------------ SUBTRACT ------------------

        public Quantity<U> Subtract(Quantity<U> other)
        {
            return Subtract(other, this.Unit);
        }

        public Quantity<U> Subtract(Quantity<U> other, U targetUnit)
        {
            double baseResult = PerformBaseArithmetic(other, ArithmeticOperation.Subtract);
            double targetValue = targetUnit.ConvertFromBaseUnit(baseResult);

            return new Quantity<U>(targetValue, targetUnit);
        }

        // ------------------ DIVIDE ------------------

        public double Divide(Quantity<U> other)
        {
            return PerformBaseArithmetic(other, ArithmeticOperation.Divide);
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

        public override string ToString()
        {
            return $"Quantity({Value}, {Unit.GetUnitName()})";
        }
    }
}