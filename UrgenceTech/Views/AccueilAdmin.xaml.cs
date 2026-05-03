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
        private AccueilViewModel testAdmin = new AccueilViewModel();

        public AccueilAdmin()
        {
            InitializeComponent();
            testAdmin.StatusChanged += () => StatusText.Text = testAdmin.Statut;
            testAdmin.SessionExpired += OnSessionExpired;
            testAdmin.LogoutRequested += OnLogout;

            StatusText.Text = testAdmin.Statut;


        }

        private void OnUserActivity(object sender, RoutedEventArgs e)
        {
            testAdmin.UserActivity();
        }

        private void OnLogout(object sender, RoutedEventArgs e)
        {
            testAdmin.Logout();
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