using System.Windows;
using ProiectPractica.Repositories;

namespace ProiectPractica.Views
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string email = EmailTextBox.Text;

            // Validate inputs
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            // Register user
            var userRepository = new UserRepository();
            bool isRegistered = userRepository.RegisterUser(username, password, email);

            if (isRegistered)
            {
                MessageBox.Show("Registration successful!");
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Registration failed. Please try again.");
            }
        }

        private void BackToLoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
