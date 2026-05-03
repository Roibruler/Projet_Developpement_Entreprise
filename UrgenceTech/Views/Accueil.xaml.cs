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
        private readonly AccueilViewModel _accueilViewModel;


        public Accueil(AccueilViewModel accueilViewModel)
        {
            InitializeComponent();

            _accueilViewModel = accueilViewModel;
            DataContext = _accueilViewModel;

            _accueilViewModel.StatusChanged += () => StatusText.Text = _accueilViewModel.Statut;
            _accueilViewModel.SessionExpired += OnSessionExpired;
            _accueilViewModel.LogoutRequested += OnLogout;

            StatusText.Text = _accueilViewModel.Statut;


        }

      

        private void OnUserActivity(object sender, RoutedEventArgs e)
        {
            _accueilViewModel.UserActivity();
        }

        private void OnLogout(object sender, RoutedEventArgs e)
        {
            _accueilViewModel.Logout();
        }

        private void OnSessionExpired()
        {
            Dispatcher.Invoke(() =>
            {
                MessageBox.Show("Votre session a expiré.");
                System.Windows.Application.Current.Shutdown();
            });
        }

        private void OnLogout()
        {
            MessageBox.Show("Déconnexion réussie.");
            Window.GetWindow(this).Close();


        }
    }

}