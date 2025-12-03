using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ProiectPractica.Helpers;
using ProiectPractica.Services;

namespace ProiectPractica.ViewModels
{
    public class LeaseCarVM : BasePropertyChanged
    {
        private readonly int _carId;
        private readonly IContractService _contractService;
        private readonly Window _leaseCarWindow;
        private DateTime _startDate;
        private DateTime _endDate;

        public LeaseCarVM(int carId, IContractService contractService, Window leaseCarWindow)
        {
            _carId = carId;
            _contractService = contractService;
            _leaseCarWindow = leaseCarWindow;
            StartDate = DateTime.Today;
            EndDate = DateTime.Today.AddYears(2);
            LeaseCommand = new RelayCommand<object>(_ => ExecuteLease());
            CancelCommand = new RelayCommand<object>(_ => _leaseCarWindow.Close());
        }

        public DateTime StartDate
        {
            get => _startDate;
            set { _startDate = value; NotifyPropertyChanged(nameof(StartDate)); }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set { _endDate = value; NotifyPropertyChanged(nameof(EndDate)); }
        }

        public ICommand LeaseCommand { get; }
        public ICommand CancelCommand { get; }

        private void ExecuteLease()
        {
            if (EndDate <= StartDate)
            {
                MessageBox.Show("End date must be after start date.");
                return;
            }

            // Assuming the user is logged in and we have the user ID
            int userId = 1; // Replace with actual logged-in user's ID

            bool success = _contractService.CreateContract(userId, _carId, StartDate, EndDate);

            if (success)
            {
                MessageBox.Show("Car leased successfully!");
                _leaseCarWindow.Close();
            }
            else
            {
                MessageBox.Show("Failed to lease car. Please try again.");
            }
        }
    }
}
