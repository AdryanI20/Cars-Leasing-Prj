using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Npgsql;
using ProiectPractica.Models;

namespace ProiectPractica.Services
{
    public interface IContractService : INotifyPropertyChanged
    {
        ObservableCollection<LeasingContract> CurrentContracts { get; }
        bool CreateContract(int userId, int carId, DateTime startDate, DateTime endDate);
    }

    public class ContractService : IContractService
    {
        private ObservableCollection<LeasingContract> _currentContracts;
        private readonly DatabaseHelper _databaseHelper;
        public event PropertyChangedEventHandler PropertyChanged;

        public ContractService()
        {
            _databaseHelper = new DatabaseHelper();
            CurrentContracts = new ObservableCollection<LeasingContract>(GetCurrentContracts());
        }

        public ObservableCollection<LeasingContract> CurrentContracts
        {
            get { return _currentContracts; }
            set { _currentContracts = value; OnPropertyChanged(nameof(CurrentContracts)); }
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
                if (rowsAffected > 0)
                {
                    CurrentContracts = new ObservableCollection<LeasingContract>(GetCurrentContracts());
                    return true;
                }
            }
            return false;
        }

        private List<LeasingContract> GetCurrentContracts()
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

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
