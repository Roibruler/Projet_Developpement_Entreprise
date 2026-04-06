using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace UrgenceTech.Views
{
    public partial class SignUp : Page
    {
        // (base de donne comme test pour les emails existants pour plustard faut le delete apres qu'on a une BD)
        private static List<string> FakeDatabaseEmails = new List<string>()
        {
            "test@gmail.com",
            "admin@urgence.com"
        };

        public SignUp()
        {
            InitializeComponent();
        }

        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            MessageErreur.Text = string.Empty;

            if (!IsEmailValid())
                return;

            if (!IsPasswordValid())
                return;

            if (EmailExists())
            {
                MessageErreur.Text =
                    "Cet email existe déjà.\n" +
                    "Veuillez vous connecter.\n" +
                    "Ou utilisez 'Mot de passe oublié ?'";
                return;
            }

            FakeDatabaseEmails.Add(Email.Text);

            MessageBox.Show("Compte créé avec succès !");

            NavigationService.Navigate(new SignUp());
        }

        private bool EmailExists()
        {
            return FakeDatabaseEmails.Contains(Email.Text.ToLower());
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

        private void GoToSignUp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SignUp());
        }

        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Fonctionnalité à venir.");
        }
    }
}