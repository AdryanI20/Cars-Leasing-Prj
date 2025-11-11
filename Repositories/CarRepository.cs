using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Media.Imaging;
using Npgsql;
using ProiectPractica;
using ProiectPractica.Models;

namespace ProiectPractica.Repositories
{
    public class CarRepository
    {
        private readonly DatabaseHelper _databaseHelper;

        public CarRepository()
        {
            _databaseHelper = new DatabaseHelper();
        }

        public List<Car> GetAvailableCars()
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
                        Uri resourceUri = new Uri(reader.GetString(reader.GetOrdinal("ImageUrl")), UriKind.Relative);
                        try
                        {
                            BitmapImage a = new BitmapImage(resourceUri);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        cars.Add(new Car
                        {
                            CarId = reader.GetInt32(reader.GetOrdinal("CarId")),
                            Model = reader.GetString(reader.GetOrdinal("Model")),
                            Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                            Year = reader.GetInt32(reader.GetOrdinal("Year")),
                            ImageUri = new BitmapImage(resourceUri)
                        });
                    }
                }
            }

            return cars;
        }

        public List<Car> FilterCars(string searchText, string filterOption)
        {
            List<Car> cars = GetAvailableCars();

            if (!string.IsNullOrEmpty(searchText))
            {
                cars = cars.FindAll(car => car.Model.ToLower().Contains(searchText.ToLower()));
            }

            switch (filterOption)
            {
                case "Price (Low to High)":
                    cars.Sort((x, y) => x.Price.CompareTo(y.Price));
                    break;
                case "Price (High to Low)":
                    cars.Sort((x, y) => y.Price.CompareTo(x.Price));
                    break;
                case "Year (Newest First)":
                    cars.Sort((x, y) => y.Year.CompareTo(x.Year));
                    break;
                case "Year (Oldest First)":
                    cars.Sort((x, y) => x.Year.CompareTo(y.Year));
                    break;
            }

            return cars;
        }

        public Car GetCarById(int carId)
        {
            using (var connection = _databaseHelper.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Cars WHERE CarId = @carId";
                command.Parameters.Add(new NpgsqlParameter("@carId", carId));

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Uri resourceUri = new Uri(reader.GetString(reader.GetOrdinal("ImageUrl")), UriKind.Relative);
                        return new Car
                        {
                            CarId = reader.GetInt32(reader.GetOrdinal("CarId")),
                            Model = reader.GetString(reader.GetOrdinal("Model")),
                            Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                            Year = reader.GetInt32(reader.GetOrdinal("Year")),
                            ImageUri = new BitmapImage(resourceUri)
                        };
                    }
                }
            }

            return null;
        }
    }
}
