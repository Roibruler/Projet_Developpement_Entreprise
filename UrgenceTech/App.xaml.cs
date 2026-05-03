using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using UrgenceTech.Data;
using UrgenceTech.ViewModels;
using UrgenceTech.Views;

namespace UrgenceTech
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ShutdownMode = ShutdownMode.OnLastWindowClose;

            // Applique les migrations et crée la base de données si elle n'existe pas
            using var context = new AppDbContext();
            context.Database.Migrate();

            var services = new ServiceCollection();

            // Register SessionManager as Singleton - available to all pages
            services.AddSingleton<SessionManager>();

            // Register ViewModels
            services.AddTransient<AccueilViewModel>();

          
            ServiceProvider = services.BuildServiceProvider();
            // Show main window from service provider

            var mainWindow = new MainWindow
            {
                DataContext = ServiceProvider.GetRequiredService<AccueilViewModel>()
            };
            MainWindow = mainWindow;
            mainWindow.Show();
        }

        internal void Show()
        {
            throw new NotImplementedException();
        }


        protected override void OnExit(ExitEventArgs e)
        {
            if (ServiceProvider is IDisposable disposable)
                disposable.Dispose();

            base.OnExit(e);
        }

  


    }
}
