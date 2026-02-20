using System;

namespace QuantityMeasurement.Core.Models
{
    public sealed class Feet
    {
        private readonly double _value;
        public double Value => _value;

        public Feet(double value)
        {
            if (value < 0)
                throw new ArgumentException("Feet cannot be negative");

            _value = value;
        }

        public override bool Equals(object obj)
        {
            if (obj is Feet other)
                return Math.Abs(_value - other._value) < 0.0001;

            return false;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{_value} ft";
        }
    }
}