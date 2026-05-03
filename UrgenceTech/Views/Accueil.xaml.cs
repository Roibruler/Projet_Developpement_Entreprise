using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UrgenceTech.ViewModels;

namespace UrgenceTech.Views
{
    /// <summary>
    /// Logique d'interaction pour Test.xaml
    /// </summary>
    public partial class Accueil : Page
    {

        private AccueilViewModel test = new AccueilViewModel();

        public Accueil()
        {
            InitializeComponent();

            test.StatusChanged += () => StatusText.Text = test.Statut;
            test.SessionExpired += OnSessionExpired;
            test.LogoutRequested += OnLogout;

            StatusText.Text = test.Statut;


        }

      

        private void OnUserActivity(object sender, RoutedEventArgs e)
        {
            test.UserActivity();
        }

        private void OnLogout(object sender, RoutedEventArgs e)
        {
            test.Logout();
        }

        private void OnSessionExpired()
        {
            Dispatcher.Invoke(() =>
            {
                MessageBox.Show("Votre session a expiré.");
                Window.GetWindow(this).Close();
            });
        }

        private void OnLogout()
        {
            MessageBox.Show("Déconnexion réussie.");
            Window.GetWindow(this).Close();


        }
    }

}