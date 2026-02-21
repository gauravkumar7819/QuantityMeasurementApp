using QuantityMeasurement.Core.Models;

namespace QuantityMeasurement.Core
{
    public class QuantityMeasurementApp
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
    }
}