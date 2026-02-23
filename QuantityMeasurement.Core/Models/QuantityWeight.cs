using System;
using QuantityMeasurement.Core.Enums;

namespace QuantityMeasurement.Core.Models
{
    public sealed class QuantityWeight
    {
        private const double Epsilon = 1e-6;

        public double Value { get; }
        public WeightUnit Unit { get; }

        public QuantityWeight(double value, WeightUnit unit)
        {
           
            if (!double.IsFinite(value))
                throw new ArgumentException("Value must be a finite number");

            Unit = unit; 
            Value = value;
        }

        private double ToKilograms() => Unit.ConvertToBaseUnit(Value);

        public QuantityWeight ConvertTo(WeightUnit targetUnit)
        {
            double kg = ToKilograms();
            double targetValue = targetUnit.ConvertFromBaseUnit(kg);
            return new QuantityWeight(targetValue, targetUnit);
        }

        public QuantityWeight Add(QuantityWeight other)
        {
            return Add(other, this.Unit);
        }

        public QuantityWeight Add(QuantityWeight other, WeightUnit targetUnit)
        {
            if (other is null) throw new ArgumentNullException(nameof(other));

            double sumKg = this.ToKilograms() + other.ToKilograms();
            double targetValue = targetUnit.ConvertFromBaseUnit(sumKg);

            return new QuantityWeight(targetValue, targetUnit);
        }

public override bool Equals(object? obj)
{
    QuantityWeight other = obj as QuantityWeight;
    if (other == null)
        return false;

    return Math.Abs(this.ToKilograms() - other.ToKilograms()) <= Epsilon;
}

        public override int GetHashCode()
        {
            // normalize to kg for stable hashing (rounded to epsilon scale)
            double kg = ToKilograms();
            long bucket = (long)Math.Round(kg / Epsilon);
            return HashCode.Combine(bucket);
        }

        public override string ToString() => $"Quantity({Value}, {Unit})";
    }
}