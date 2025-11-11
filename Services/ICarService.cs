using System.Collections.ObjectModel;
using System.ComponentModel;
using ProiectPractica.Helpers;
using ProiectPractica.Models;

namespace ProiectPractica.Services
{
    public interface ICarService : INotifyPropertyChanged
    {
        ObservableCollection<Car> AvailableCars { get; }
        void FilterCars(string searchText, string filterOption);
    }

    public class CarService : ICarService
    {
        private ObservableCollection<Car> _availableCars;
        private readonly DatabaseHelper _databaseHelper;
        public event PropertyChangedEventHandler PropertyChanged;

        public CarService()
        {
            _databaseHelper = new DatabaseHelper();
            AvailableCars = new ObservableCollection<Car>(GetAvailableCars());
        }

        public ObservableCollection<Car> AvailableCars
        {
            get { return _availableCars; }
            set { _availableCars = value; OnPropertyChanged(nameof(AvailableCars)); }
        }

        public void FilterCars(string searchText, string filterOption)
        {
            var cars = GetAvailableCars();

            if (!string.IsNullOrEmpty(searchText))
            {
                cars = cars.Where(car => car.Model.ToLower().Contains(searchText.ToLower())).ToList();
            }

            switch (filterOption)
            {
                case "Price (Low to High)":
                    cars = cars.OrderBy(car => car.Price).ToList();
                    break;
                case "Price (High to Low)":
                    cars = cars.OrderByDescending(car => car.Price).ToList();
                    break;
                case "Year (Newest First)":
                    cars = cars.OrderByDescending(car => car.Year).ToList();
                    break;
                case "Year (Oldest First)":
                    cars = cars.OrderBy(car => car.Year).ToList();
                    break;
            }

            AvailableCars = new ObservableCollection<Car>(cars);
        }

        private List<Car> GetAvailableCars()
        {
            List<Car> cars = new List<Car>();

            using (var connection = _databaseHelper.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Cars";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cars.Add(new Car
                        {
                            CarId = reader.GetInt32(reader.GetOrdinal("CarId")),
                            Model = reader.GetString(reader.GetOrdinal("Model")),
                            Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                            Year = reader.GetInt32(reader.GetOrdinal("Year")),
                            ImageUri = "/"+reader.GetString(reader.GetOrdinal("ImageUrl"))
                        });
                    }
                }
            }

            return cars;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
