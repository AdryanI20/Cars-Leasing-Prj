using System.Windows;
using System.Windows.Controls;
using ProiectPractica.Repositories;
using System.Collections.Generic;
using ProiectPractica.Models;

namespace ProiectPractica.Views
{
    public partial class CurrentContractsWindow : Page
    {
        private ContractRepository _contractRepository;

        public CurrentContractsWindow()
        {
            InitializeComponent();
            _contractRepository = new ContractRepository();
            LoadCurrentContracts();
        }

        private void LoadCurrentContracts()
        {
            List<LeasingContract> contracts = _contractRepository.GetCurrentContracts();
            CurrentContractsListView.ItemsSource = contracts;
        }
    }
}
