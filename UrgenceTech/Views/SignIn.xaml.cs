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

namespace UrgenceTech.Views
{
    /// <summary>
    /// Logique d'interaction pour SignIn.xaml
    /// </summary>
    public partial class SignIn : Page
    {
        public SignIn()
        {
            InitializeComponent();
        }


        private void SignInBtn_Click(object sender, RoutedEventArgs e)
        {

            ErreurBorder.Visibility = Visibility.Collapsed;
            MessageErreur.Text = string.Empty;

            if (!IsEmailValid()) 
            { 
                return; 
            }

            if (!IsPasswordValid())
            {
                return;
            }

            NavigationService.Navigate(new Test()); // Aller à la page d'accueil après une connexion réussie
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
