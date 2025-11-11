using System.Windows;

namespace ProiectPractica.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AvailableCarsButton_Click(object sender, RoutedEventArgs e)
        {
            AvailableCarsWindow availableCarsWindow = new AvailableCarsWindow();
            MainFrame.Navigate(availableCarsWindow);
        }

        private void CurrentContractsButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentContractsWindow currentContractsWindow = new CurrentContractsWindow();
            MainFrame.Navigate(currentContractsWindow);
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
