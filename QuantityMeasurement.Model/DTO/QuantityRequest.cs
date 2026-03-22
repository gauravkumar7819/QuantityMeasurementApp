namespace QuantityMeasurement.Model.DTO
{
    public class QuantityRequest
    {
        public QuantityDTO Q1 { get; set; } = new();
        public QuantityDTO Q2 { get; set; } = new();
        public string TargetUnit { get; set; } = string.Empty;

        public QuantityRequest() { }

        public QuantityRequest(QuantityDTO q1, QuantityDTO q2, string targetUnit = "")
        {
            Q1 = q1;
            Q2 = q2;
            TargetUnit = targetUnit;
        }
    }
}
