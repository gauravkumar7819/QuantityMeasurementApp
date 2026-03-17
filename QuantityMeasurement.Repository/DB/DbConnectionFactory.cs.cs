using System.Data.SqlClient;

namespace QuantityMeasurement.Repository.DB
{
    public static class DbConnectionFactory
    {
        private static readonly string connectionString =
            "Server=localhost\\SQLEXPRESS;Database=QuantityMeasurementDB;Trusted_Connection=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}