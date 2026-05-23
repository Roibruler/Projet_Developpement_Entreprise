using Microsoft.EntityFrameworkCore;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UrgenceTech.Data;
using UrgenceTech.ViewModels;

namespace UrgenceTech.Views
{
    internal partial class SignIn : Page
    {
        private const int MaxTentatives = 5;
        private const int DureeVerrouillage = 15;

        private readonly AppDbContext _context;

        internal SignIn(AppDbContext context)
        {
            InitializeComponent();
            _context = context;
        }

        private async void SignInBtn_Click(object sender, RoutedEventArgs e)
        {
            ErreurBorder.Visibility = Visibility.Collapsed;
            MessageErreur.Text = string.Empty;

            if (!IsEmailValid()) return;
            if (!IsPasswordValid()) return;

            DemarrerChargement();

            try
            {
                await Task.Delay(3000);

                var utilisateur = await _context.Utilisateurs
                    .FirstOrDefaultAsync(u => u.Courriel == Email.Text);

                if (utilisateur == null)
                {
                    AfficherErreur("Identifiants incorrects.");
                    return;
                }

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
                        utilisateur.DateVerrouillage = null;
                        utilisateur.TentativesEchouees = 0;
                        await _context.SaveChangesAsync();
                    }
                }

                if (!AuthService.VerifierMotDePasse(MotPasse.Password, utilisateur.MotDePasse!))
                {
                    utilisateur.TentativesEchouees++;

                    if (utilisateur.TentativesEchouees >= MaxTentatives)
                    {
                        utilisateur.DateVerrouillage = DateTime.Now;
                        await _context.SaveChangesAsync();
                        AfficherErreur("Compte verrouillé après 5 tentatives. Réessayez dans 15 minutes.");
                        return;
                    }

                    int tentativesRestantes = MaxTentatives - utilisateur.TentativesEchouees;
                    await _context.SaveChangesAsync();
                    AfficherErreur($"Mot de passe incorrect. {tentativesRestantes} tentative(s) restante(s).");
                    return;
                }

                utilisateur.TentativesEchouees = 0;
                utilisateur.DateVerrouillage = null;
                await _context.SaveChangesAsync();

                AuthService.SeConnecter(utilisateur.Courriel!, MotPasse.Password);

                var sessionManager = new SessionManager();
                sessionManager.StartSession();

                var sessionManagerViewModel = new SessionManagerViewModel(sessionManager);

                var menu = new MenuView(utilisateur, sessionManagerViewModel);
                menu.Show();

                Window.GetWindow(this)?.Close();
            }
            finally
            {
                ArreterChargement();
            }
        }

        private void GoToSignUpBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SignUp());
        }

        private void DemarrerChargement()
        {
            ChargementOverlay.Visibility = Visibility.Visible;
            SignInBtn.IsEnabled = false;
        }

        private void ArreterChargement()
        {
            ChargementOverlay.Visibility = Visibility.Collapsed;
            SignInBtn.IsEnabled = true;
        }

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