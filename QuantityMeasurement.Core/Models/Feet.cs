namespace QuantityMeasurement.Core.Models
{
    public class Feet
    {
        public double Value { get; private set; }

       public Feet(double value)
{
    if (value < 0)
        throw new ArgumentException("Feet value cannot be negative");

    Value = value;
}

        public bool Equals(Feet other)
        {
            if (other == null) return false;
            return this.Value == other.Value;
        }
    }
}