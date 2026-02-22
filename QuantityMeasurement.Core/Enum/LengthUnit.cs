namespace QuantityMeasurement.Core.Enums
{
    public enum LengthUnit
    {
        INCHES = 1,          // 1 inch = 1 inch
        FEET = 12,           // 1 foot = 12 inches
        YARDS = 36,          // 1 yard = 36 inches
        CENTIMETERS = 0      // handled separately (cannot store double in enum)
    }
}