namespace QuantityMeasurement.Model.Models
{
    public class Inches
    {
        public double Value { get; private set; }

public Inches(double value)
{
    if (value < 0)
        throw new ArgumentException("Inches value cannot be negative");

    Value = value;
}

        public bool Equals(Inches other)
        {
            if (other == null) return false;
            return this.Value == other.Value;
        }
    }
}