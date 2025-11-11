using Npgsql;
using System;
using System.Data;

namespace ProiectPractica
{
    public class DatabaseHelper
    {
        private string connectionString;

        public DatabaseHelper()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CarLeasingDB"].ConnectionString;
        }

        public IDbConnection GetConnection()
        {
            return new NpgsqlConnection(connectionString);
        }
    }
}
