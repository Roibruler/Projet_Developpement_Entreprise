using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using UrgenceTech.Views;

namespace UrgenceTech
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ShutdownMode = ShutdownMode.OnLastWindowClose;
        }
    }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            NavigationWindow window = new NavigationWindow();
            window.Source = new Uri("views/SignUp.xaml", UriKind.Relative);
            window.Show();
        }

        internal void Show()
        {
            throw new NotImplementedException();
        }
    }
}