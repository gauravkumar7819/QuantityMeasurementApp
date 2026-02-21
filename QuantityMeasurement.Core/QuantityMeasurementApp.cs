using QuantityMeasurement.Core.Models;

namespace QuantityMeasurement.Core
{
    public class QuantityMeasurementApp
    {
        // UC1: Feet equality check
        public static bool AreFeetEqual(double firstFeet, double secondFeet)
        {
            var feet1 = new Feet(firstFeet);
            var feet2 = new Feet(secondFeet);

            return feet1.Equals(feet2);
        }
    }
}