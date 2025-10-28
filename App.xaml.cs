using System.Configuration;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Windows;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using AutoLeasingApp.Data;
using Microsoft.EntityFrameworkCore;
using ProiectPractica.Services;

namespace ProiectPractica
{
    public partial class App : Application
    {
        private IHost? _host;

        public App()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            IConfiguration configuration = builder.Build();

            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IConfiguration>(configuration);
                    services.AddDbContext<AppDbContext>(options =>
                        options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

                    services.AddScoped<IUserService, UserService>();
                    services.AddScoped<ILeasingService, LeasingService>();
                })
                .Build();
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            var scope = _host.Services.CreateScope();
            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
            System.Diagnostics.Debug.WriteLine($"UserService resolved: {userService.GetType().Name}");

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            if (_host != null)
            {
                await _host.StopAsync();
                _host.Dispose();
            }
            base.OnExit(e);
        }
    }

}
