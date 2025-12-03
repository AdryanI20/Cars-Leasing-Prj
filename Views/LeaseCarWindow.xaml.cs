using System;
using System.Windows;
using ProiectPractica.Models;
using ProiectPractica.Services;
using ProiectPractica.ViewModels;

namespace ProiectPractica.Views
{
    public partial class LeaseCarWindow : Window
    {
        private readonly int _carId;
        private readonly IContractService _contractService;

        public LeaseCarWindow(int carId)
        {
            InitializeComponent();
            _carId = carId;
            _contractService = new ContractService();
            DataContext = new LeaseCarVM(_carId, _contractService, this);
        }

    }
}
