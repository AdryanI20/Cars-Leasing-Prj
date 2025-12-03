using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using ProiectPractica.Helpers;
using ProiectPractica.Models;
using ProiectPractica.Services;
using ProiectPractica.Views;

namespace ProiectPractica.ViewModels
{
    public class AvailableCarsVM : BasePropertyChanged
    {
        private readonly ICarService _carService;
        private string _searchText;
        private string _selectedFilter;

        public AvailableCarsVM(ICarService carService)
        {
            _carService = carService;
            ApplyFilterCommand = new RelayCommand<object>(_ => ApplyFilter());
            LeaseCarCommand = new RelayCommand<int>(carId => LeaseCar(carId));
        }

        public ObservableCollection<Car> AvailableCars => _carService.AvailableCars;

        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value; NotifyPropertyChanged(nameof(SearchText)); }
        }

        public string SelectedFilter
        {
            get { return _selectedFilter; }
            set { _selectedFilter = value; NotifyPropertyChanged(nameof(SelectedFilter)); }
        }

        public ICommand ApplyFilterCommand { get; }
        public ICommand LeaseCarCommand { get; }

        private void ApplyFilter()
        {
            _carService.FilterCars(SearchText, SelectedFilter);
        }

        private void LeaseCar(int carId)
        {
            LeaseCarWindow leaseCarWindow = new LeaseCarWindow(carId);
            leaseCarWindow.ShowDialog();
        }
    }
}
