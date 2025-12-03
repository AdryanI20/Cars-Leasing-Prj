using System.Windows;
using System.Windows.Controls;
using ProiectPractica.Services;
using ProiectPractica.ViewModels;

namespace ProiectPractica.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            IUserService userService = new UserService();
            DataContext = new LoginVM(userService, this);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).Password = ((PasswordBox)sender).SecurePassword;
            }
        }
    }
}
