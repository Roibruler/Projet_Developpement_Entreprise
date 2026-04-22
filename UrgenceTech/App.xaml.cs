using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using UrgenceTech.Views;
using Microsoft.EntityFrameworkCore;
using UrgenceTech.Data;

namespace UrgenceTech
{

    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ShutdownMode = ShutdownMode.OnLastWindowClose;

            // Applique les migrations et crée la base de données si elle n'existe pas
            using var context = new AppDbContext();
            context.Database.Migrate();
        }

        internal void Show()
        {
            throw new NotImplementedException();
        }
    }
}
