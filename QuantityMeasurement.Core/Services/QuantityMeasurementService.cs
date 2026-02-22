using System;
using QuantityMeasurement.Core.Enums;
using QuantityMeasurement.Core.Models;

namespace QuantityMeasurement.Core.Service
{
    public class QuantityMeasurementService
    {
        // UC1: Feet equality
        public static bool AreFeetEqual(double firstFeet, double secondFeet)
        {
            var feet1 = new Feet(firstFeet);
            var feet2 = new Feet(secondFeet);
            return feet1.Equals(feet2);
        }

        // UC2: Inches equality
        public static bool AreInchesEqual(double firstInches, double secondInches)
        {
            var inch1 = new Inches(firstInches);
            var inch2 = new Inches(secondInches);
            return inch1.Equals(inch2);
        }

        // UC3/UC4: Compare any two values with units (DRY)
        public static bool AreQuantitiesEqual(double value1, LengthUnit unit1, double value2, LengthUnit unit2)
        {
            var length1 = new Length(value1, unit1);
            var length2 = new Length(value2, unit2);
            return length1.Equals(length2);
        }

        // UC3: Compare any two Length values (instance wrapper - keeps compatibility with your tests)
        public static  bool AreLengthsEqual(double value1, LengthUnit unit1, double value2, LengthUnit unit2)
            => AreQuantitiesEqual(value1, unit1, value2, unit2);

        public static bool AreEqual(QuantityLength first, QuantityLength second)
            => first.Equals(second);

        // Demo helpers
        public static void DemonstrateLengthConversion(double value, LengthUnit from, LengthUnit to)
        {
            double result = QuantityLength.Convert(value, from, to);
            Console.WriteLine($"\nConverted Value: {result} {to}");
        }

        public static void DemonstrateLengthConversion(QuantityLength length, LengthUnit to)
        {
            QuantityLength converted = length.ConvertTo(to);
            Console.WriteLine($"\nConverted Value: {converted}");
        }

        public static void DemonstrateLengthEquality(QuantityLength first, QuantityLength second)
        {
            Console.WriteLine(first.Equals(second)
                ? "\nLengths are Equal "
                : "\nLengths are Not Equal ");
        }
    }
}