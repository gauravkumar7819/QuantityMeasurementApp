using System;
using QuantityMeasurement.Core.Enums;

namespace QuantityMeasurement.Core.Models
{
    public class QuantityLength
    {
        private readonly double _value;
        private readonly LengthUnit _unit;

        private const double EPSILON = 1e-6;
        private const double INCH_TO_CM = 2.54;   // exact definition

        public QuantityLength(double value, LengthUnit unit)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Invalid numeric value");

            _value = value;
            _unit = unit;
        }

        public double Value => _value;
        public LengthUnit Unit => _unit;

        // ---------- STATIC CONVERT ----------
        public static double Convert(double value, LengthUnit source, LengthUnit target)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Invalid numeric value");

            double inches = ConvertToInches(value, source);
            return ConvertFromInches(inches, target);
        }

        // ---------- INSTANCE METHOD ----------
        public QuantityLength ConvertTo(LengthUnit targetUnit)
        {
            double converted = Convert(_value, _unit, targetUnit);
            return new QuantityLength(converted, targetUnit);
        }

        // ---------- PRIVATE HELPERS ----------
        private static double ConvertToInches(double value, LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.INCHES => value,
                LengthUnit.FEET => value * 12,
                LengthUnit.YARDS => value * 36,
                LengthUnit.CENTIMETERS => value / INCH_TO_CM,
                _ => throw new ArgumentException("Unsupported unit")
            };
        }

        private static double ConvertFromInches(double inches, LengthUnit target)
        {
            return target switch
            {
                LengthUnit.INCHES => inches,
                LengthUnit.FEET => inches / 12,
                LengthUnit.YARDS => inches / 36,
                LengthUnit.CENTIMETERS => inches * INCH_TO_CM,
                _ => throw new ArgumentException("Unsupported unit")
            };
        }

        // ---------- EQUALITY ----------
        public override bool Equals(object? obj)
        {
            if (obj is not QuantityLength other)
                return false;

            double thisInches = ConvertToInches(_value, _unit);
            double otherInches = ConvertToInches(other._value, other._unit);

            return Math.Abs(thisInches - otherInches) < EPSILON;
        }

        public override int GetHashCode()
        {
            return ConvertToInches(_value, _unit).GetHashCode();
        }

        public override string ToString()
        {
            return $"{_value} {_unit}";
        }
    }
}