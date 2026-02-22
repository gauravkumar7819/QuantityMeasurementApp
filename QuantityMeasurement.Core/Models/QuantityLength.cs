using System;
using QuantityMeasurement.Core.Enums;

namespace QuantityMeasurement.Core.Models
{
    public class QuantityLength
    {
        private readonly double _value;
        private readonly LengthUnit _unit;

        private const double CM_TO_INCH = 0.393701;
        private const double PRECISION = 0.00001;


        public QuantityLength(double value, LengthUnit unit)
        {
            if (value < 0)
                throw new ArgumentException("Length cannot be negative");

            _value = value;
            _unit = unit;
        }

        private double ConvertToInches()
        {
            if (_unit == LengthUnit.CENTIMETERS)
                return _value * CM_TO_INCH;

            return _value * (int)_unit;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj.GetType() != typeof(QuantityLength))
                return false;

            QuantityLength other = (QuantityLength)obj;

            double thisInches = this.ConvertToInches();
            double otherInches = other.ConvertToInches();

            return Math.Abs(thisInches - otherInches) < PRECISION;
        }

        public override int GetHashCode()
        {
            return ConvertToInches().GetHashCode();
        }
    }
}