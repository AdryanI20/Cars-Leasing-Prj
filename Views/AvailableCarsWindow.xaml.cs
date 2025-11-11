using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ProiectPractica.Repositories;
using ProiectPractica.Models;

namespace ProiectPractica.Views
{
    public partial class AvailableCarsWindow : Page
    {
        private CarRepository _carRepository;

        public AvailableCarsWindow()
        {
            InitializeComponent();
            _carRepository = new CarRepository();
            LoadAvailableCars();
        }

        private void LoadAvailableCars()
        {
            List<Car> cars = _carRepository.GetAvailableCars();
            AvailableCarsListView.ItemsSource = cars;
        }

        private void ApplyFilterButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text;
            string filterOption = (FilterComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            List<Car> filteredCars = _carRepository.FilterCars(searchText, filterOption);
            AvailableCarsListView.ItemsSource = filteredCars;
        }

        private void LeaseButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int carId = (int)button.Tag;

            LeaseCarWindow leaseCarWindow = new LeaseCarWindow(carId);
            leaseCarWindow.ShowDialog();
        }
    }
}
