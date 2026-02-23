using System;
using QuantityMeasurement.Core.Enums;
using QuantityMeasurement.Core.Models;

namespace QuantityMeasurement.Core.Service
{
    public class QuantityMeasurementService
    {
        // For LengthTests (expects: FEET/INCHES/etc)
        private static void ThrowIfNegative(double value, LengthUnit unit)
        {
            if (value < 0)
                throw new ArgumentException($"{unit} cannot be negative");
        }

        // ------------------ UC1: Feet equality ------------------

        public static bool AreFeetEqual(double firstFeet, double secondFeet)
        {
            // FeetTests expects: "Feet cannot be negative"
            if (firstFeet < 0 || secondFeet < 0)
                throw new ArgumentException("Feet cannot be negative");

            var feet1 = new QuantityLength(firstFeet, LengthUnit.FEET);
            var feet2 = new QuantityLength(secondFeet, LengthUnit.FEET);

            return feet1.Equals(feet2);
        }

        // ------------------ UC2: Inches equality ------------------

        public static bool AreInchesEqual(double firstInches, double secondInches)
        {
            // InchesTests expects: "Inches cannot be negative"
            if (firstInches < 0 || secondInches < 0)
                throw new ArgumentException("Inches cannot be negative");

            var inch1 = new QuantityLength(firstInches, LengthUnit.INCHES);
            var inch2 = new QuantityLength(secondInches, LengthUnit.INCHES);

            return inch1.Equals(inch2);
        }

        // ------------------ UC3/UC4: Generic equality ------------------

        public static bool AreQuantitiesEqual(
            double value1, LengthUnit unit1,
            double value2, LengthUnit unit2)
        {
            // LengthTests expects unit-specific messages:
            // "FEET cannot be negative" or "INCHES cannot be negative"
            ThrowIfNegative(value1, unit1);
            ThrowIfNegative(value2, unit2);

            var length1 = new QuantityLength(value1, unit1);
            var length2 = new QuantityLength(value2, unit2);

            return length1.Equals(length2);
        }

        // Instance wrapper (backward compatibility)
        public bool AreLengthsEqual(
            double value1, LengthUnit unit1,
            double value2, LengthUnit unit2)
            => AreQuantitiesEqual(value1, unit1, value2, unit2);

        public bool AreEqual(QuantityLength first, QuantityLength second)
            => first.Equals(second);

        // ------------------ DEMO HELPERS ------------------

        public static void DemonstrateLengthConversion(
            double value, LengthUnit from, LengthUnit to)
        {
            // keep consistent with unit-based behavior
            ThrowIfNegative(value, from);

            var length = new QuantityLength(value, from);
            var converted = length.ConvertTo(to);

            Console.WriteLine($"\nConverted Value: {converted}");
        }

        public static void DemonstrateLengthConversion(
            QuantityLength length, LengthUnit to)
        {
            if (length is null) throw new ArgumentNullException(nameof(length));
            ThrowIfNegative(length.Value, length.Unit);

            QuantityLength converted = length.ConvertTo(to);

            Console.WriteLine($"\nConverted Value: {converted}");
        }

        public static void DemonstrateLengthEquality(
            QuantityLength first, QuantityLength second)
        {
            if (first is null) throw new ArgumentNullException(nameof(first));
            if (second is null) throw new ArgumentNullException(nameof(second));

            Console.WriteLine(first.Equals(second)
                ? "\nLengths are Equal"
                : "\nLengths are Not Equal");
        }
    }
}