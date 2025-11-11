using System;
using System.Collections.Generic;
using System.Data;
using ProiectPractica.Models;
using Npgsql;
using ProiectPractica;
using ProiectPractica.Models;

namespace ProiectPractica.Repositories
{
    public class ContractRepository
    {
        private readonly DatabaseHelper _databaseHelper;

        public ContractRepository()
        {
            _databaseHelper = new DatabaseHelper();
        }

        public List<LeasingContract> GetCurrentContracts()
        {
            List<LeasingContract> contracts = new List<LeasingContract>();

            using (var connection = _databaseHelper.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM LeasingContracts";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        contracts.Add(new LeasingContract
                        {
                            ContractId = reader.GetInt32(reader.GetOrdinal("ContractId")),
                            UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                            CarId = reader.GetInt32(reader.GetOrdinal("CarId")),
                            StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate")),
                            EndDate = reader.GetDateTime(reader.GetOrdinal("EndDate")),
                            Status = reader.GetString(reader.GetOrdinal("Status"))
                        });
                    }
                }
            }

            return contracts;
        }

        public bool CreateContract(int userId, int carId, DateTime startDate, DateTime endDate)
        {
            using (var connection = _databaseHelper.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO LeasingContracts (UserId, CarId, StartDate, EndDate, Status) VALUES (@userId, @carId, @startDate, @endDate, @status)";
                command.Parameters.Add(new NpgsqlParameter("@userId", userId));
                command.Parameters.Add(new NpgsqlParameter("@carId", carId));
                command.Parameters.Add(new NpgsqlParameter("@startDate", startDate));
                command.Parameters.Add(new NpgsqlParameter("@endDate", endDate));
                command.Parameters.Add(new NpgsqlParameter("@status", "Active"));

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}
