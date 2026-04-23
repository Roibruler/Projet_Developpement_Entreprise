using System.Windows;
using System.Windows.Controls;
using UrgenceTech.Services;

namespace UrgenceTech.Views
{
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void BtnInscrire_Click(object sender, RoutedEventArgs e)
        {
            // Récupérer les valeurs
            string prenom = TxtPrenom.Text.Trim();
            string nom = TxtNom.Text.Trim();
            string courriel = TxtCourriel.Text.Trim();
            string motDePasse = TxtMotDePasse.Password;
            string confirmation = TxtConfirmation.Password;

            // --- Validation des champs ---
            if (string.IsNullOrEmpty(prenom) || string.IsNullOrEmpty(nom) ||
                string.IsNullOrEmpty(courriel) || string.IsNullOrEmpty(motDePasse))
            {
                AfficherErreur("Tous les champs sont obligatoires.");
                return;
            }

            if (!courriel.Contains("@") || !courriel.Contains("."))
            {
                AfficherErreur("Le format du courriel est invalide.");
                return;
            }

            if (motDePasse.Length < 6)
            {
                AfficherErreur("Le mot de passe doit contenir au moins 6 caractères.");
                return;
            }

            if (motDePasse != confirmation)
            {
                AfficherErreur("Les mots de passe ne correspondent pas.");
                return;
            }

            // --- Tentative de création de compte ---
            var utilisateur = AuthService.CreerCompte(nom, prenom, courriel, motDePasse, nouvelUtilisateur);

            if (utilisateur == null)
            {
                AfficherErreur("Ce courriel est déjà utilisé. Veuillez en choisir un autre.");
                return;
            }

            // --- Succès : confirmation visuelle + redirection ---
            MessageBox.Show(
                $"Bienvenue, {utilisateur.Prenom} !\nVotre compte a été créé avec succès.",
                "Compte créé",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );

            // L'utilisateur est déjà connecté via AuthService.CreerCompte()
            // Ouvrir le tableau de bord et fermer cette fenêtre
            var dashboard = new MainWindow();
            dashboard.Show();
            this.Close();
        }

        private void LienConnexion_Click(object sender, RoutedEventArgs e)
        {
            var signIn = new SignIn();
            signIn.Show();
            this.Close();
        }

        private void AfficherErreur(string message)
        {
            TxtErreur.Text = message;
        }
    }
}