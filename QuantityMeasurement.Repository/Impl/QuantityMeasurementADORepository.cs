using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using QuantityMeasurement.Model.Entities;
using QuantityMeasurement.Repository.Interfaces;

namespace QuantityMeasurement.Repository.Implementations
{
    public class QuantityMeasurementADORepository : IQuantityMeasurementRepository
    {
        private const string ConnectionString = "Server=localhost\\SQLEXPRESS;Database=QuantityMeasurementDB;Trusted_Connection=True;TrustServerCertificate=True";
        private const string TableName = "QuantityMeasurements";

        public void Save(QuantityMeasurementEntity entity)
        {
            using var conn = new SqlConnection(ConnectionString);
            var query = $"INSERT INTO {TableName} (Operation, Operand1, Operand2, Result, HasError, ErrorMessage, TimeStamp) " +
                        "VALUES (@op, @o1, @o2, @res, @err, @msg, @time)";
            
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@op", entity.Operation);
            cmd.Parameters.AddWithValue("@o1", entity.Operand1);
            cmd.Parameters.AddWithValue("@o2", entity.Operand2);
            cmd.Parameters.AddWithValue("@res", (object?)entity.Result ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@err", entity.HasError);
            cmd.Parameters.AddWithValue("@msg", (object?)entity.ErrorMessage ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@time", entity.TimeStamp);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public List<QuantityMeasurementEntity> GetAll()
        {
            var list = new List<QuantityMeasurementEntity>();
            using var conn = new SqlConnection(ConnectionString);
            using var cmd = new SqlCommand($"SELECT * FROM {TableName}", conn);
            
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new QuantityMeasurementEntity
                {
                    Operation = reader["Operation"]?.ToString() ?? "",
                    Operand1 = reader["Operand1"]?.ToString() ?? "",
                    Operand2 = reader["Operand2"]?.ToString() ?? "",
                    Result = reader["Result"]?.ToString(),
                    HasError = (bool)reader["HasError"],
                    ErrorMessage = reader["ErrorMessage"]?.ToString() ?? "",
                    TimeStamp = (DateTime)reader["TimeStamp"]
                });
            }
            return list;
        }

        public void DeleteAll()
        {
            using var conn = new SqlConnection(ConnectionString);
            using var cmd = new SqlCommand($"DELETE FROM {TableName}", conn);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}