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
    public class RegisterVM : BasePropertyChanged
    {
        private readonly IUserService _userService;
        private readonly Window _registerWindow;
        private string _username;
        private SecureString _password;
        private string _email;

        public RegisterVM(IUserService userService, Window registerWindow)
        {
            _userService = userService;
            _registerWindow = registerWindow;
            RegisterCommand = new RelayCommand<object>(_ => ExecuteRegister());
            BackToLoginCommand = new RelayCommand<object>(_ => ExecuteBackToLogin());
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

        public string Email
        {
            get => _email;
            set { _email = value; NotifyPropertyChanged(nameof(Email)); }
        }

        public ICommand RegisterCommand { get; }
        public ICommand BackToLoginCommand { get; }

        private void ExecuteRegister()
        {
            if (string.IsNullOrEmpty(Username) || Password == null || Password.Length == 0 || string.IsNullOrEmpty(Email))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            string password = new System.Net.NetworkCredential(string.Empty, Password).Password;

            // simulate success
            MessageBox.Show("Registration successful!");
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            _registerWindow.Close();
        }

        private void ExecuteBackToLogin()
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            _registerWindow.Close();
        }
    }
}
