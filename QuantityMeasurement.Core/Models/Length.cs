using System;
using QuantityMeasurement.Core.Enums;

namespace QuantityMeasurement.Core.Models
{
    public class Length
    {
        public double Value { get; private set; }
        public LengthUnit Unit { get; private set; }

        public Length(double value, LengthUnit unit)
        {
            if (value < 0)
                throw new ArgumentException($"{unit} cannot be negative");

            Value = value;
            Unit = unit;
        }

        // Convert to base unit (inches)
        private double ConvertToBaseUnit()
        {
            return Value * (int)Unit;  // Enum int value = conversion factor
        }

        // Compare two Length objects
        public bool Compare(Length other)
        {
            if (other == null) return false;
            return ConvertToBaseUnit() == other.ConvertToBaseUnit();
        }

        // Override Equals
// Length.cs
public override bool Equals(object? obj)
{
    return obj is Length other && Equals(other);
}

        public override int GetHashCode()
        {
            return ConvertToBaseUnit().GetHashCode();
        }

        public override string ToString()
        {
            return $"{Value} {Unit}";
        }
    }
}