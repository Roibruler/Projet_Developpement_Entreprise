using System.Windows;
using System.Windows.Controls;
using UrgenceTech.Models;
using UrgenceTech.Views;

namespace UrgenceTech.Views
{
    public partial class MenuView : Window
    {
        private readonly Utilisateur _utilisateurConnecte;

        public MenuView(Utilisateur utilisateur)
        {
            InitializeComponent();
            _utilisateurConnecte = utilisateur;
            ContenuPrincipal.Content = new TableauDeBordView();
        }

        private void NavTableauDeBord_Click(object sender, RoutedEventArgs e)
        {
            ContenuPrincipal.Content = new TableauDeBordView();
        }

        private void NavUrgences_Click(object sender, RoutedEventArgs e)
        {
        }

        private void NavUtilisateurs_Click(object sender, RoutedEventArgs e)
        {
        }

        private void NavProfil_Click(object sender, RoutedEventArgs e)
        {
            ContenuPrincipal.Content = new ProfilView(_utilisateurConnecte);
        }

        private void Deconnexion_Click(object sender, RoutedEventArgs e)
        {
            var login = new LoginView();
            login.Show();
            this.Close();
        }
    }
}