using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;

using Microsoft.EntityFrameworkCore;
using GROUP7WPF;
using FUMiniTikiSystem.DAL;
using Microsoft.Extensions.Configuration;
using FUMiniTikiSystem.DAL.Interfaces;
using FUMiniTikiSystem.DAL.Repositories;
using FUMiniTikiSystem.BLL.Services;
using FUMiniTikiSystem.BLL.Interfaces;

namespace FUMiniTikiSystem.WPF
{
    public partial class App : Application
    {
        public static IHost AppHost { get; private set; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // Register DbContext from appsettings.json
                    var config = context.Configuration;
                    services.AddDbContext<FuminiTikiSystemContext>(options =>
                        options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

                    services.AddScoped<ICustomerRepository,CustomerRepository>();
                    services.AddScoped<IUnitOfWork, UnitOfWork>();

                    // Add Services
                    services.AddScoped<ICustomerService, CustomerService>();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost.StartAsync();

            // Mở MainWindow
            var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost.StopAsync();
            base.OnExit(e);
        }
    }
}
