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
using static System.Collections.Specialized.BitVector32;

namespace UrgenceTech.Views
{
    /// <summary>
    /// Interaction logic for VoirUtilisateur.xaml
    /// </summary>
    public partial class VoirUtilisateur : Page
    {

        private AccueilViewModel testAdmin;
        public VoirUtilisateur(AccueilViewModel accueilViewModel,
        VoirUtilisateurViewModel voirUtilisateurViewModel)
        {
            InitializeComponent();


            DataContext = voirUtilisateurViewModel;


            testAdmin = accueilViewModel;


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
