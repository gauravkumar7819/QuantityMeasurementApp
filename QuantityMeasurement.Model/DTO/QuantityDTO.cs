using System;

namespace QuantityMeasurement.Model.DTO
{
    public class QuantityDTO
    {
        public double Value { get; set; }

        public string Unit { get; set; } = string.Empty;


        public QuantityDTO()
        {
        }

        public QuantityDTO(double value, string unit)
        {
            Value = value;
            Unit = unit;
        }

        public override string ToString()
        {
            return $"{Value} {Unit}";
        }
    }
}