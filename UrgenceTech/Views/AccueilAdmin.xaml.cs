using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using static System.Net.Mime.MediaTypeNames;

namespace UrgenceTech.Views
{
    /// <summary>
    /// Interaction logic for TestAdmin.xaml
    /// </summary>
    public partial class AccueilAdmin : Page
    {

        private readonly SessionManagerViewModel _sessionManagerViewModel;

        public AccueilAdmin(SessionManagerViewModel sessionManagerViewModel)
        {
            InitializeComponent();

            _sessionManagerViewModel = sessionManagerViewModel;
            DataContext = _sessionManagerViewModel;

            _sessionManagerViewModel.SessionExpired += OnSessionExpired;
            _sessionManagerViewModel.LogoutRequested += OnLogout;


        }

        private void OnUserActivity(object sender, RoutedEventArgs e)
        {
            _sessionManagerViewModel.UserActivity();
        }

        private void OnLogout(object sender, RoutedEventArgs e)
        {
            _sessionManagerViewModel.Logout();
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

        private void GotoVoirUtilisateur_Click(object sender, RoutedEventArgs e)
        {

            var page = App.ServiceProvider.GetRequiredService<VoirUtilisateur>();
            NavigationService.Navigate(page);

        }

    }

}