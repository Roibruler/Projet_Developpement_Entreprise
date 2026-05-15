using CommunityToolkit.Mvvm.ComponentModel;
using UrgenceTech.Models;

namespace UrgenceTech.ViewModels
{
    internal partial class ProfilViewModel : ObservableObject
    {
        [ObservableProperty]
        private string nomComplet = string.Empty;

        [ObservableProperty]
        private string courriel = string.Empty;

        [ObservableProperty]
        private string role = string.Empty;

        [ObservableProperty]
        private string dateCreation = string.Empty;

        public ProfilViewModel(Utilisateur utilisateur)
        {
            NomComplet = utilisateur.NomComplet ?? string.Empty;
            Courriel = utilisateur.Courriel ?? string.Empty;
            Role = utilisateur.Role ?? string.Empty;
            DateCreation = utilisateur.DateCreation.ToString("dd/MM/yyyy");
        }
    }
}