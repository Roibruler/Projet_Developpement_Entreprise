using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void AccepterConditions_Changed(object sender, RoutedEventArgs e)
        {
            CreerCompteBTN.IsEnabled = AccepterConditions.IsChecked == true;
        }

        private void LienConditions_Click(object sender, RoutedEventArgs e)
        {
            ConditionsWindow fenetre = new ConditionsWindow();
            fenetre.Owner = Window.GetWindow(this);
            fenetre.ShowDialog();
        }

        private void CreerCompteBTN_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Email.Text) || Email.Text == "Email" || string.IsNullOrWhiteSpace(MotPasse.Text) || MotPasse.Text == "Mot de passe" || string.IsNullOrWhiteSpace(ConfirmerMotPasse.Text) || ConfirmerMotPasse.Text == "Confirmer mot de passe" || string.IsNullOrWhiteSpace(Email.Text) || Email.Text == "Email" || string.IsNullOrWhiteSpace(NomComplet.Text) || NomComplet.Text == "Nom Complet")
            {
                MessageErreurCreationCompte.Text = "Les champs: " + "\n" + "Email" + "\n" + "Mot de passe" + "\n" + "Confirmer mot de passe" + "\n" + "Nom complet ne sont pas remplie";
                return;
            }
            if (string.IsNullOrWhiteSpace(ConfirmerMotPasse.Text) || ConfirmerMotPasse.Text == "Confirmer mot de passe" || string.IsNullOrWhiteSpace(NomComplet.Text) || NomComplet.Text == "Nom Complet")
            {
                MessageErreurCreationCompte.Text = "Les champs Confirmer mot de passe et Nom complet ne sont pas remplie";
                return;
            }
            if (string.IsNullOrWhiteSpace(MotPasse.Text) || MotPasse.Text == "Mot de passe" || string.IsNullOrWhiteSpace(NomComplet.Text) || NomComplet.Text == "Nom Complet")
            {
                MessageErreurCreationCompte.Text = "Les champs Mot de passe et Nom Complet ne sont pas remplie";
                return;
            }
            if (string.IsNullOrWhiteSpace(MotPasse.Text) || MotPasse.Text == "Mot de passe" || string.IsNullOrWhiteSpace(ConfirmerMotPasse.Text) || ConfirmerMotPasse.Text == "Confirmer mot de passe")
            {
                MessageErreurCreationCompte.Text = "Les champs Mot de passe et Confirmer mot de passe ne sont pas remplie";
                return;
            }
            if (string.IsNullOrWhiteSpace(Email.Text) || Email.Text == "Email" || string.IsNullOrWhiteSpace(NomComplet.Text) || NomComplet.Text == "Nom Complet")
            {
                MessageErreurCreationCompte.Text = "Les champs Email et Nom complet ne sont pas remplie";
                return;
            }
            if (string.IsNullOrWhiteSpace(Email.Text) || Email.Text == "Email" || string.IsNullOrWhiteSpace(ConfirmerMotPasse.Text) || ConfirmerMotPasse.Text == "Confirmer mot de passe")
            {
                MessageErreurCreationCompte.Text = "Les champs Email et Confimrer mot de passe ne sont pas remplie";
                return;
            }
            if (string.IsNullOrWhiteSpace(Email.Text) || Email.Text == "Email" || string.IsNullOrWhiteSpace(MotPasse.Text) || MotPasse.Text == "Mot de passe")
            {
                MessageErreurCreationCompte.Text = "Les champs Email et Mot de passe ne sont pas remplie";
                return;
            }
            if (string.IsNullOrWhiteSpace(NomComplet.Text) || NomComplet.Text == "Nom Complet")
            {
                MessageErreurCreationCompte.Text = "Le champs Nom complet n'est pas remplie";
                return;
            }
            if (string.IsNullOrWhiteSpace(ConfirmerMotPasse.Text) || ConfirmerMotPasse.Text == "Confirmer mot de passe")
            {
                MessageErreurCreationCompte.Text = "Le champs Confirmer mot de passe n'est pas remplie";
                return;
            }
            if (string.IsNullOrWhiteSpace(MotPasse.Text) || MotPasse.Text == "Mot de passe")
            {
                MessageErreurCreationCompte.Text = "Le champs Mot de passe n'est pas remplie";
                return;
            }
            if (string.IsNullOrWhiteSpace(Email.Text) || Email.Text == "Email")
            {
                MessageErreurCreationCompte.Text = "Le champs Email n'est pas remplie";
                return;
            }

            // Check if all password criteria are met
            if (!_criteriaViewModel.IsPasswordValid)
            {
                MessageErreurCreationCompte.Text = "Le mot de passe ne respecte pas tous les critères";
                return;
            }

            // Check if passwords match
            if (MotPasse.Text != ConfirmerMotPasse.Text)
            {
                MessageErreurCreationCompte.Text = "Les mots de passe ne correspondent pas";
                return;
            }

            MessageErreurCreationCompte.Text = string.Empty;

            MainWindow main = new MainWindow();
            main.Show();

            Window.GetWindow(this).Close();
        }

        private void RetirerTexte(object sender, RoutedEventArgs e)
        {
            TextBox texbox = (TextBox)sender;

            if (texbox.Tag as string == "Email")
            {
                texbox.Text = "";
            }
            else if (texbox.Tag as string == "Mot de passe")
            {
                texbox.Text = "";
            }
            else if (texbox.Tag as string == "Confirmer mot de passe")
            {
                texbox.Text = "";
            }
            else if (texbox.Tag as string == "Nom Complet")
            {
                texbox.Text = "";
            }

            // si est sur le textbox motpasse - visibilité = peut voir 

            TextBox MotPasse = sender as TextBox;


            if (texbox.Name == "MotPasse") 
            {
                CriètreMotDePasse.Visibility = Visibility.Visible;
            } 
            

        }


        // si n'est pas sur le textbox motpasse - visibilité = peut pas voir 
        private void AjouterTexte(object sender, RoutedEventArgs e)
        {
            TextBox textbox = (TextBox)sender;

            if (string.IsNullOrWhiteSpace(textbox.Text))
            {
                textbox.Text = textbox.Tag as string;
            }

            if (textbox.Name == "MotPasse")
            {
                CriètreMotDePasse.Visibility = Visibility.Collapsed;
            }
            
        }

        //update les critères du mot de passe
        private void MotPasse_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox motPasse = sender as TextBox;

            if (motPasse != null && motPasse.Name == "MotPasse")
            {
                
                if (motPasse.Text != "Mot de passe" && motPasse.Text != "")
                {
                    _criteriaViewModel.MotDePasse = motPasse.Text;
                }
                else if (motPasse.Text == "")
                {
                    _criteriaViewModel.MotDePasse = "";
                }
            }
        }


        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is CritèreViewModel critère)
            {
                critère.MotDePasse = ((PasswordBox)sender).Password;
            }
        }


    }
}
