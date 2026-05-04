using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UrgenceTech.ViewModels;

namespace UrgenceTech.Views
{
    public partial class SignUp : Page
    {
        private CritèreViewModel _criteriaViewModel;

        public SignUp()
        {
            InitializeComponent();
            _criteriaViewModel = new CritèreViewModel();
            DataContext = _criteriaViewModel;
        }

        private async void CreerCompteBTN_Click(object sender, RoutedEventArgs e)
        {
            MessageErreurCreationCompte.Text = string.Empty;

            string nomComplet = NomComplet.Text.Trim();
            string courriel = Email.Text.Trim();
            string motDePasse = MotPasse.Password;
            string confirmation = ConfirmerMotPasse.Password;

            if (string.IsNullOrWhiteSpace(nomComplet))
            {
                MessageErreurCreationCompte.Text = "Le champ Nom complet est obligatoire.";
                return;
            }
            if (string.IsNullOrWhiteSpace(courriel))
            {
                MessageErreurCreationCompte.Text = "Le champ Courriel est obligatoire.";
                return;
            }
            if (string.IsNullOrWhiteSpace(motDePasse))
            {
                MessageErreurCreationCompte.Text = "Le champ Mot de passe est obligatoire.";
                return;
            }
            if (string.IsNullOrWhiteSpace(confirmation))
            {
                MessageErreurCreationCompte.Text = "Le champ Confirmer mot de passe est obligatoire.";
                return;
            }
            if (!courriel.Contains("@") || !courriel.Contains("."))
            {
                MessageErreurCreationCompte.Text = "Format de courriel invalide.";
                return;
            }
            if (motDePasse.Length < 8)
            {
                MessageErreurCreationCompte.Text = "Le mot de passe doit contenir au moins 8 caractères.";
                return;
            }
            if (motDePasse != confirmation)
            {
                MessageErreurCreationCompte.Text = "Les mots de passe ne correspondent pas.";
                return;
            }

            var (succes, erreur) = await AuthService.CreerCompteAsync(nomComplet, courriel, motDePasse);

            if (!succes)
            {
                MessageErreurCreationCompte.Text = erreur;
                return;
            }

            var loginWindow = new LoginView();
            loginWindow.Show();
            Window.GetWindow(this)?.Close();
        }
    }
}