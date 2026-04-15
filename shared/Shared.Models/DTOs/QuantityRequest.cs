namespace Shared.Models.DTOs
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

    public class OperationLog
    {
        public int Id { get; set; }
        public string Operation { get; set; } = string.Empty;
        public QuantityDTO Q1 { get; set; } = new();
        public QuantityDTO? Q2 { get; set; }
        public string TargetUnit { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
        public string? UserEmail { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
