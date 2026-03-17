namespace QuantityMeasurement.Repository.DB
{
    public static class DbConstants
    {
        public const string TABLE_NAME = "QuantityMeasurements";

        public const string INSERT_QUERY = @"
        INSERT INTO QuantityMeasurements
        (Operation, Operand1, Operand2, Result, HasError, ErrorMessage, TimeStamp)
        VALUES
        (@operation, @operand1, @operand2, @result, @hasError, @errorMsg, @timeStamp)";
    }
}