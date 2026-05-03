using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
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

            //injection de Dépendance
           
            var services = new ServiceCollection();

            //Registrer Base de Donné
            using var context = new AppDbContext();
            services.AddDbContext<AppDbContext>();

         

            // Register ViewModels
            services.AddTransient<AccueilViewModel>();
            services.AddTransient<VoirUtilisateurViewModel>();

            //Registre Views
            services.AddTransient<Accueil>();
            services.AddTransient<AccueilAdmin>(); 
            services.AddTransient<VoirUtilisateur>();
            services.AddTransient<SignIn>();

            //Registrer Singleton
            services.AddSingleton<SessionManager>();


            ServiceProvider = services.BuildServiceProvider();
            // Show main window from service provider

            context.Database.Migrate();

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
