using System.Windows;
using System.Windows.Controls;
using UrgenceTech.ViewModels;

namespace UrgenceTech.Views
{
    public partial class MenuView : Window
    {

        private SessionManagerViewModel _sessionManagerViewModel;

        public MenuView(SessionManagerViewModel sessionManagerViewModel)
        {
            InitializeComponent();
            ContenuPrincipal.Content = new TableauDeBordView();

            _sessionManagerViewModel = sessionManagerViewModel;
            DataContext = _sessionManagerViewModel;

            _sessionManagerViewModel.SessionExpired += OnSessionExpired;


        }

        private void NavTableauDeBord_Click(object sender, RoutedEventArgs e)
        {
            ContenuPrincipal.Content = new TableauDeBordView();
        }

        private void NavUrgences_Click(object sender, RoutedEventArgs e)
        {
            ContenuPrincipal.Content = new UrgenceView();
        }

        private void NavUtilisateurs_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NavConsultation_Click(object sender, RoutedEventArgs e)
        {
            ContenuPrincipal.Content = new VoirUtilisateurView();
        }

        private void Deconnexion_Click(object sender, RoutedEventArgs e)
        {
            AuthService.SeDeconnecter();
            var login = new LoginView();
            login.Show();
            this.Close();
        }

        private void OnUserActivity(object sender, RoutedEventArgs e)
        {
            _sessionManagerViewModel.UserActivity();
        }


        private void OnSessionExpired()
        {
            Dispatcher.Invoke(() =>
            {
                MessageBox.Show("Votre session a expiré.");
                System.Windows.Application.Current.Shutdown();
            });
        }


        public void OuvrirCreerUrgence()
        {
            var vue = new CreerUrgenceView();
            vue.ShowDialog();
        }

        public void OuvrirUrgencesEnCours()
        {
            var vue = new UrgencesEnCoursView();
            vue.ShowDialog();
        }
    }
}