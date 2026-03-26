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

        }

        private bool IsEmailValid()
        {
            if (string.IsNullOrEmpty(Email.Text) || !IsValidEmailFormat())
            {
                MessageErreur.Text = "Le courriel est invalide.";
                return false;
            }

            if (Email.Text.Length > 100)
            {
                MessageErreur.Text = "Le courriel est trop long.";
                return false;
            }

            return true;
        }

        private bool IsPasswordValid()
        {
            if (string.IsNullOrEmpty(MotPasse.Password))
            {
                MessageErreur.Text = "Le mot de passe est invalide.";
                return false;
            }

            if (MotPasse.Password.Length > 50)
            {
                MessageErreur.Text = "Le mot de passe est trop long.";
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
