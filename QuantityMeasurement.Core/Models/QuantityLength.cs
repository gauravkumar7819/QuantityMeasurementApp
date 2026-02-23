using System;
using QuantityMeasurement.Core.Enums;

namespace QuantityMeasurement.Core.Models
{
    public class QuantityLength
    {
        private readonly double _value;
        private readonly LengthUnit _unit;

        private const double INCH_TO_CM = 2.54;
        private const double EPSILON = 1e-6;


public QuantityLength(double value, LengthUnit unit)
        {
            if (value < 0)
                throw new ArgumentException($"{unit} cannot be negative"); // FEET / INCHES / YARDS

            _value = value;
            _unit = unit;
        }
        public double Value => _value;
        public LengthUnit Unit => _unit;

        // ------------------ ADD METHOD (UC6) ------------------

        public QuantityLength Add(QuantityLength other)
        {
            if (other is null)
                throw new ArgumentException("Second operand cannot be null");

            // Convert both to inches (base unit)
            double thisInches = ConvertToInches(_value, _unit);
            double otherInches = ConvertToInches(other._value, other._unit);

            // Add
            double sumInches = thisInches + otherInches;

            // Convert back to unit of first operand
            double resultValue = ConvertFromInches(sumInches, _unit);

            return new QuantityLength(resultValue, _unit);
        }
        public QuantityLength ConvertTo(LengthUnit targetUnit)
{
    double converted = Convert(_value, _unit, targetUnit);
    return new QuantityLength(converted, targetUnit);
}

        // Optional static overload
        public static QuantityLength Add(QuantityLength first, QuantityLength second)
        {
            if (first is null || second is null)
                throw new ArgumentException("Operands cannot be null");

            return first.Add(second);
        }

        // ------------------ CONVERSION ------------------

        public static double Convert(double value, LengthUnit source, LengthUnit target)
        {
            double inches = ConvertToInches(value, source);
            return ConvertFromInches(inches, target);
        }

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
// ------------------ ADD METHODS ------------------



// UC7 (explicit target unit)
public QuantityLength Add(QuantityLength other, LengthUnit targetUnit)
{
    if (other is null)
        throw new ArgumentException("Second operand cannot be null");

    if (!Enum.IsDefined(typeof(LengthUnit), targetUnit))
        throw new ArgumentException("Invalid target unit");

    double resultValue = AddInternal(this, other, targetUnit);

    return new QuantityLength(resultValue, targetUnit);
}

// Static overload
public static QuantityLength Add(QuantityLength first, QuantityLength second, LengthUnit targetUnit)
{
    if (first is null || second is null)
        throw new ArgumentException("Operands cannot be null");

    return first.Add(second, targetUnit);
}

// ------------------ PRIVATE UTILITY METHOD ------------------

private static double AddInternal(QuantityLength first, QuantityLength second, LengthUnit targetUnit)
{
    // Convert both to inches (base unit)
    double firstInches = ConvertToInches(first._value, first._unit);
    double secondInches = ConvertToInches(second._value, second._unit);

    double sumInches = firstInches + secondInches;

    // Convert to explicit target unit
    return ConvertFromInches(sumInches, targetUnit);
}
        // ------------------ EQUALITY ------------------

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
            return $"Quantity({_value}, {_unit})";
        }
    
    }
}