using System;
using System.ComponentModel.DataAnnotations; 

namespace QuantityMeasurement.Model.Entities
{
    public class QuantityMeasurementEntity
    {
        [Key] 
        public int Id { get; set; }

        public string Operation { get; set; } = string.Empty;

        public string Operand1 { get; set; } = string.Empty;

        public string Operand2 { get; set; } = string.Empty;

        public string? Result { get; set; } // nullable better

        public bool HasError { get; set; }

        public string? ErrorMessage { get; set; } // nullable better

        public DateTime TimeStamp { get; set; }

        public QuantityMeasurementEntity()
        {
            TimeStamp = DateTime.Now;
        }

        public QuantityMeasurementEntity(
            string operation,
            string operand1,
            string operand2,
            string result)
        {
            Operation = operation;
            Operand1 = operand1;
            Operand2 = operand2;
            Result = result;
            HasError = false;
            TimeStamp = DateTime.Now;
        }

        public QuantityMeasurementEntity(
            string operation,
            string operand1,
            string operand2,
            string errorMessage,
            bool hasError)
        {
            Operation = operation;
            Operand1 = operand1;
            Operand2 = operand2;
            ErrorMessage = errorMessage;
            HasError = hasError;
            TimeStamp = DateTime.Now;
        }

        public override string ToString()
        {
            if (HasError)
            {
                return $"[{TimeStamp}] ERROR during {Operation}: {ErrorMessage}";
            }

            return $"[{TimeStamp}] {Operand1} {Operation} {Operand2} = {Result}";
        }
    }
}