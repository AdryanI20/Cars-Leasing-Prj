using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ProiectPractica.Helpers;
using ProiectPractica.Services;
using ProiectPractica.Views;

namespace ProiectPractica.ViewModels
{
    public class LoginVM : BasePropertyChanged
    {
        private readonly IUserService _userService;
        private readonly Window _loginWindow;
        private string _username;
        private SecureString _password;

        public LoginVM(IUserService userService, Window loginWindow)
        {
            _userService = userService;
            _loginWindow = loginWindow;
            LoginCommand = new RelayCommand<object>(_ => ExecuteLogin());
            RegisterCommand = new RelayCommand<object>(_ => ExecuteRegister());
        }

        public string Username
        {
            get => _username;
            set { _username = value; NotifyPropertyChanged(nameof(Username)); }
        }

        public SecureString Password
        {
            get => _password;
            set { _password = value; NotifyPropertyChanged(nameof(Password)); }
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        private void ExecuteLogin()
        {
            if (string.IsNullOrEmpty(Username) || Password == null || Password.Length == 0)
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            string password = new System.Net.NetworkCredential(string.Empty, Password).Password;

            bool isAuthenticated = _userService.AuthenticateUser(Username, password);

            if (isAuthenticated)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                _loginWindow.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        private void ExecuteRegister()
        {
            RegisterWindow registerWindow = new RegisterWindow(_userService);
            registerWindow.Show();
            _loginWindow.Close();
        }
    }
}
