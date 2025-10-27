using System.Configuration;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Windows;

namespace ProiectPractica
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IConfiguration? _configuration;

        public App()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true);
            _configuration = builder.Build();

            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            System.Diagnostics.Debug.WriteLine($"Connection String: {_configuration.GetConnectionString("DefaultConnection")}");
        }
    }

}
