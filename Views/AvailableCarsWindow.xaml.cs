using System.Windows;
using System.Windows.Controls;
using ProiectPractica.Services;
using ProiectPractica.ViewModels;

namespace ProiectPractica.Views
{
    public partial class AvailableCarsWindow : Page
    {
        public AvailableCarsWindow()
        {
            InitializeComponent();
            DataContext = new AvailableCarsVM(new CarService());
        }
    }
}
