using System.ComponentModel;
using Npgsql;
using ProiectPractica.Helpers;

namespace ProiectPractica.Services
{
    public interface IUserService : INotifyPropertyChanged
    {
        int? CurrentUserId { get; set; }
        bool AuthenticateUser(string username, string password);
    }

    public class UserService : IUserService
    {
        private int? _currentUserId;
        private readonly DatabaseHelper _databaseHelper;
        public event PropertyChangedEventHandler PropertyChanged;

        public UserService()
        {
            _databaseHelper = new DatabaseHelper();
        }

        public int? CurrentUserId
        {
            get => _currentUserId;
            set
            {
                _currentUserId = value;
                OnPropertyChanged(nameof(CurrentUserId));
            }
        }

        public bool AuthenticateUser(string username, string password)
        {
            using (var connection = _databaseHelper.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT UserId FROM Users WHERE Username = @username AND Password = @password";
                command.Parameters.Add(new NpgsqlParameter("@username", username));
                command.Parameters.Add(new NpgsqlParameter("@password", password));

                var result = command.ExecuteScalar();
                if (result != null)
                {
                    CurrentUserId = (int)result;
                    return true;
                }
            }
            return false;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
