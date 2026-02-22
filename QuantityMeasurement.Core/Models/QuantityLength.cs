using System;
using QuantityMeasurement.Core.Enums;

namespace QuantityMeasurement.Core.Models
{
    public class QuantityLength
    {
        private readonly double _value;
        private readonly LengthUnit _unit;

        private const double PRECISION = 1e-6;
        private const double CM_TO_INCH = 0.393701;

        public QuantityLength(double value, LengthUnit unit)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Invalid numeric value");

            _value = value;
            _unit = unit;
        }

        public double Value => _value;
        public LengthUnit Unit => _unit;

        // ---------- STATIC CONVERT METHOD (API) ----------
        public static double Convert(double value, LengthUnit source, LengthUnit target)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Invalid numeric value");

            double valueInInches = ConvertToBase(value, source);
            return ConvertFromBase(valueInInches, target);
        }

        // ---------- INSTANCE METHOD ----------
        public QuantityLength ConvertTo(LengthUnit targetUnit)
        {
            double converted = Convert(_value, _unit, targetUnit);
            return new QuantityLength(converted, targetUnit);
        }

        // ---------- PRIVATE BASE CONVERSION ----------
        private static double ConvertToBase(double value, LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.INCHES => value,
                LengthUnit.FEET => value * 12,
                LengthUnit.YARDS => value * 36,
                LengthUnit.CENTIMETERS => value * CM_TO_INCH,
                _ => throw new ArgumentException("Unsupported unit")
            };
        }

        private static double ConvertFromBase(double baseValue, LengthUnit target)
        {
            return target switch
            {
                LengthUnit.INCHES => baseValue,
                LengthUnit.FEET => baseValue / 12,
                LengthUnit.YARDS => baseValue / 36,
                LengthUnit.CENTIMETERS => baseValue / CM_TO_INCH,
                _ => throw new ArgumentException("Unsupported unit")
            };
        }

        // ---------- EQUALITY ----------
        public override bool Equals(object? obj)
        {
            if (obj is not QuantityLength other)
                return false;

            double thisBase = ConvertToBase(_value, _unit);
            double otherBase = ConvertToBase(other._value, other._unit);

            return Math.Abs(thisBase - otherBase) < PRECISION;
        }

        public override int GetHashCode()
        {
            return ConvertToBase(_value, _unit).GetHashCode();
        }

        public override string ToString()
        {
            return $"{_value} {_unit}";
        }
    }
}