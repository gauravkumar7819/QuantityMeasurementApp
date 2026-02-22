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

         // UC-3: Compare any two Length object
        public  bool AreLengthsEqual(double value1, LengthUnit unit1, double value2, LengthUnit unit2)
        {
            var length1 = new Length(value1, unit1);
            var length2 = new Length(value2, unit2);

            return length1.Equals(length2);
        }
         public bool AreEqual(QuantityLength first, QuantityLength second)
        {
            return first.Equals(second);
        }
    }
}