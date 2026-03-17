using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using QuantityMeasurement.Model.Entities;
using QuantityMeasurement.Repository.Interfaces;
using QuantityMeasurement.Repository.DB;

namespace QuantityMeasurement.Repository.Implementations
{
    public class QuantityMeasurementADORepository : IQuantityMeasurementRepository
    {
        public void Save(QuantityMeasurementEntity entity)
        {
            using (SqlConnection conn = DbConnectionFactory.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(DbConstants.INSERT_QUERY, conn);

                cmd.Parameters.AddWithValue("@operation", entity.Operation);
                cmd.Parameters.AddWithValue("@operand1", entity.Operand1);
                cmd.Parameters.AddWithValue("@operand2", entity.Operand2);
                cmd.Parameters.AddWithValue("@result",
                    (object?)entity.Result ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@hasError", entity.HasError);
                cmd.Parameters.AddWithValue("@errorMsg",
                    (object?)entity.ErrorMessage ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@timeStamp", entity.TimeStamp);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<QuantityMeasurementEntity> GetAll()
        {
            List<QuantityMeasurementEntity> list = new();

            using (SqlConnection conn = DbConnectionFactory.GetConnection())
            {
                string query = $"SELECT * FROM {DbConstants.TABLE_NAME}";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string operation = reader["Operation"]?.ToString();
                    string operand1 = reader["Operand1"]?.ToString();
                    string operand2 = reader["Operand2"]?.ToString();
                    string result = reader["Result"]?.ToString();
                    bool hasError = Convert.ToBoolean(reader["HasError"]);
                    string errorMessage = reader["ErrorMessage"]?.ToString();
                    DateTime timeStamp = Convert.ToDateTime(reader["TimeStamp"]);

                    QuantityMeasurementEntity entity;

                    if (hasError)
                    {
                        entity = new QuantityMeasurementEntity(
                            operation,
                            operand1,
                            operand2,
                            errorMessage,
                            true
                        );
                    }
                    else
                    {
                        entity = new QuantityMeasurementEntity(
                            operation,
                            operand1,
                            operand2,
                            result
                        );
                    }

                    entity.TimeStamp = timeStamp;

                    list.Add(entity);
                }
            }

            return list;
        }
    }
}