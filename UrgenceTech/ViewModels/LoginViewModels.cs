using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using UrgenceTech.Data;
using UrgenceTech.Views;

namespace UrgenceTech.ViewModels
{
    internal partial class LoginViewModels : ObservableObject
    {
        [ObservableProperty]
        private string? courriel = string.Empty;

        [ObservableProperty]
        private string motDePasse = string.Empty;

        [ObservableProperty]
        private string messageErreur = string.Empty;

        [RelayCommand]
        private async Task SeConnecter()
        {
            // Effacer le message d'erreur précédent
            MessageErreur = string.Empty;

            //Veriifer si les champs sont vides
            if (string.IsNullOrEmpty(Courriel) || string.IsNullOrEmpty(MotDePasse)) 
            {
                MessageErreur = "Veuillez remplir tous les champs.";
                return;
            }

            //Chercher l'utilisateur dans la BD
            using var context = new AppDbContext();

            var utilisateur = await context.Utilisateurs
                .FirstOrDefaultAsync(u => u.Courriel == Courriel
                                    && u.MotDePasse == MotDePasse
                                    && u.Status == true);
           
            //Identifiants incorrects
            if (utilisateur == null)
            {
                MessageErreur = "Identifiants incorrects.";
                return;
            }

            //Redirection vers la page principale 
            
            var menu = new MenuView();
            menu.Show();

            // Fermer la fenêtre Login
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window is LoginView)
                {
                    window.Close();
                    break;
                }
            }
        }
    }

}
