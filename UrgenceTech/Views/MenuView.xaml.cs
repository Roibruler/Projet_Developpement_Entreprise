using System.Windows;
using System.Windows.Controls;

namespace UrgenceTech.Views
{
    public partial class MenuView : Window
    {
        public MenuView()
        {
            InitializeComponent();
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

        private void Deconnexion_Click(object sender, RoutedEventArgs e)
        {
            var login = new LoginView();
            login.Show();
            this.Close();
        }
    }
}