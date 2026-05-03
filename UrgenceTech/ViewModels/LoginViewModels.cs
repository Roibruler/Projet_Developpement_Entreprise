using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

        [ObservableProperty]
        private bool estEnChargement = false;

        [RelayCommand]
        private async Task SeConnecter()
        {
            MessageErreur = string.Empty;

            if (string.IsNullOrWhiteSpace(Courriel) || string.IsNullOrWhiteSpace(MotDePasse))
            {
                MessageErreur = "Veuillez remplir tous les champs.";
                return;
            }

            if (!Courriel.Contains("@") || !Courriel.Contains("."))
            {
                MessageErreur = "Format de courriel invalide.";
                return;
            }

            if (MotDePasse.Length < 8)
            {
                MessageErreur = "Le mot de passe doit contenir au moins 8 caractères.";
                return;
            }

            EstEnChargement = true;

            try
            {
                var (utilisateur, erreur) = await AuthService.SeConnecterAsync(Courriel, MotDePasse);

                if (utilisateur == null)
                {
                    MessageErreur = erreur;
                    return;
                }

                var menu = new MenuView();
                menu.Show();

                foreach (Window window in Application.Current.Windows)
                {
                    if (window is LoginView)
                    {
                        window.Close();
                        break;
                    }
                }
            }
            finally
            {
                EstEnChargement = false;
            }
        }
    }
}