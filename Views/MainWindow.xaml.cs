using System.Windows;

namespace ProiectPractica.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new AvailableCarsWindow());
        }

        private void AvailableCarsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AvailableCarsWindow());
        }

        private void CurrentContractsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CurrentContractsWindow());
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
