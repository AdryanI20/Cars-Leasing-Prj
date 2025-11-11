using Npgsql;

namespace ProiectPractica.Repositories
{
    public class UserRepository
    {
        private readonly DatabaseHelper _databaseHelper;

        public UserRepository()
        {
            _databaseHelper = new DatabaseHelper();
        }

        public bool AuthenticateUser(string username, string password)
        {
            using (var connection = _databaseHelper.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM Users WHERE Username = @username AND Password = @password";
                command.Parameters.Add(new NpgsqlParameter("@username", username));
                command.Parameters.Add(new NpgsqlParameter("@password", password));

                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }

        public bool RegisterUser(string username, string password, string email)
        {
            using (var connection = _databaseHelper.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Users (Username, Password, Email) VALUES (@username, @password, @email)";
                command.Parameters.Add(new NpgsqlParameter("@username", username));
                command.Parameters.Add(new NpgsqlParameter("@password", password));
                command.Parameters.Add(new NpgsqlParameter("@email", email));

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}
