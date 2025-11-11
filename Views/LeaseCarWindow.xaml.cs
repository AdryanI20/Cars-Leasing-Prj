using System;
using System.Windows;
using ProiectPractica.Models;
using ProiectPractica.Repositories;
using ProiectPractica.Models;
using ProiectPractica.Repositories;

namespace ProiectPractica.Views
{
    public partial class LeaseCarWindow : Window
    {
        private int _carId;
        private CarRepository _carRepository;
        private ContractRepository _contractRepository;

        public LeaseCarWindow(int carId)
        {
            InitializeComponent();
            _carId = carId;
            _carRepository = new CarRepository();
            _contractRepository = new ContractRepository();

            LoadCarDetails();
            SetDefaultDates();
        }

        private void LoadCarDetails()
        {
            Car car = _carRepository.GetCarById(_carId);
            if (car != null)
            {
                ModelTextBlock.Text = car.Model;
                PriceTextBlock.Text = car.Price.ToString();
            }
        }

        private void SetDefaultDates()
        {
            StartDatePicker.SelectedDate = DateTime.Today;
            EndDatePicker.SelectedDate = DateTime.Today.AddYears(2);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ConfirmLeaseButton_Click(object sender, RoutedEventArgs e)
        {
            if (StartDatePicker.SelectedDate == null || EndDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Please select both start and end dates.");
                return;
            }

            DateTime startDate = StartDatePicker.SelectedDate.Value;
            DateTime endDate = EndDatePicker.SelectedDate.Value;

            if (endDate <= startDate)
            {
                MessageBox.Show("End date must be after start date.");
                return;
            }

            // Assuming the user is logged in and we have the user ID
            int userId = 1; // This should be replaced with the actual logged-in user's ID

            bool success = _contractRepository.CreateContract(userId, _carId, startDate, endDate);

            if (success)
            {
                MessageBox.Show("Car leased successfully!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to lease car. Please try again.");
            }
        }
    }
}
