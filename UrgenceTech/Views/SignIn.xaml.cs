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
using UrgenceTech.Data;
using Microsoft.EntityFrameworkCore;

namespace UrgenceTech.Views

{
    /// <summary>
    /// Logique d'interaction pour SignIn.xaml
    /// </summary>
    public partial class SignIn : Page
    {
        // Nombre maximum de tentatives avant verrouillage
        private const int MaxTentatives = 5;

        // Durée du verrouillage en minutes
        private const int DureeVerrouillage = 15;

        public SignIn()
        {
            InitializeComponent();
        }

        private async void SignInBtn_Click(object sender, RoutedEventArgs e)
        {
            // Cacher la boîte d'erreur
            ErreurBorder.Visibility = Visibility.Collapsed;
            MessageErreur.Text = string.Empty;

            // Valider le format
            if (!IsEmailValid()) return;
            if (!IsPasswordValid()) return;

            using var context = new AppDbContext();

            // Chercher l'utilisateur
            var utilisateur = await context.Utilisateurs
                .FirstOrDefaultAsync(u => u.Courriel == Email.Text);

            // Utilisateur introuvable
            if (utilisateur == null)
            {
                AfficherErreur("Identifiants incorrects.");
                return;
            }

            // Vérifier si le compte est verrouillé
            if (utilisateur.DateVerrouillage.HasValue)
            {
                var tempsRestant = utilisateur.DateVerrouillage.Value
                    .AddMinutes(DureeVerrouillage) - DateTime.Now;

                if (tempsRestant.TotalMinutes > 0)
                {
                    AfficherErreur($"Compte verrouillé. Réessayez dans {(int)tempsRestant.TotalMinutes + 1} minute(s).");
                    return;
                }
                else
                {
                    // Déverrouiller le compte après 15 minutes
                    utilisateur.DateVerrouillage = null;
                    utilisateur.TentativesEchouees = 0;
                    await context.SaveChangesAsync();
                }
            }

            // Vérifier le mot de passe
            if (utilisateur.MotDePasse != MotPasse.Password)
            {
                utilisateur.TentativesEchouees++;

                if (utilisateur.TentativesEchouees >= MaxTentatives)
                {
                    utilisateur.DateVerrouillage = DateTime.Now;
                    await context.SaveChangesAsync();
                    AfficherErreur("Compte verrouillé après 5 tentatives. Réessayez dans 15 minutes.");
                    return;
                }

                int tentativesRestantes = MaxTentatives - utilisateur.TentativesEchouees;
                await context.SaveChangesAsync();
                AfficherErreur($"Mot de passe incorrect. {tentativesRestantes} tentative(s) restante(s).");
                return;
            }

            // Connexion réussie — réinitialiser les tentatives
            utilisateur.TentativesEchouees = 0;
            utilisateur.DateVerrouillage = null;
            await context.SaveChangesAsync();

            NavigationService.Navigate(new Test());
        }

        private void GoToSignUpBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SignUp());
        }

        // Affiche la boîte rouge avec le message d'erreur
        private void AfficherErreur(string message)
        {
            MessageErreur.Text = message;
            ErreurBorder.Visibility = Visibility.Visible;
        }

        private bool IsEmailValid()
        {
            if (string.IsNullOrEmpty(Email.Text))
            {
                AfficherErreur("Veuillez entrer votre courriel.");
                return false;
            }

            if (!IsValidEmailFormat())
            {
                AfficherErreur("Format d'email invalide.");
                return false;
            }

            if (Email.Text.Length > 100)
            {
                AfficherErreur("Le courriel est trop long.");
                return false;
            }

            return true;
        }

        private bool IsPasswordValid()
        {
            if (string.IsNullOrEmpty(MotPasse.Password))
            {
                AfficherErreur("Veuillez entrer votre mot de passe.");
                return false;
            }

            if (MotPasse.Password.Length < 8)
            {
                AfficherErreur("Le mot de passe doit contenir au moins 8 caractères.");
                return false;
            }

            if (MotPasse.Password.Length > 50)
            {
                AfficherErreur("Le mot de passe est trop long.");
                return false;
            }

            return true;
        }

        private bool IsValidEmailFormat()
        {
            string pattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
            return Regex.IsMatch(Email.Text, pattern, RegexOptions.IgnoreCase);
        }
    }
}
