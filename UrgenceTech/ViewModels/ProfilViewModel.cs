using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UrgenceTech.Models;
using UrgenceTech.Repositories;

namespace UrgenceTech.ViewModels
{
    internal partial class ProfilViewModel : ObservableObject
    {
        private readonly AuthRepository _authRepository;
        private readonly Utilisateur _oldUtilisateur;

        [ObservableProperty]
        private string nomComplet = string.Empty;

        [ObservableProperty]
        private string courriel = string.Empty;

        [ObservableProperty]
        private string role = string.Empty;

        [ObservableProperty]
        private string dateCreation = string.Empty;

        [ObservableProperty]
        private string message = string.Empty;

        public ProfilViewModel(Utilisateur utilisateur)
        {

            _authRepository = new AuthRepository();
            _oldUtilisateur = utilisateur;

            NomComplet = $"{utilisateur.NomComplet}";

            Courriel = utilisateur.Courriel ?? string.Empty;
            Role = utilisateur.Role ?? string.Empty;
            DateCreation = utilisateur.DateCreation.ToString("dd/MM/yyyy");
        }

        [RelayCommand]
        private void Sauvegarder()
        {
            Message = string.Empty;

            if(string.IsNullOrEmpty(nomComplet) || string.IsNullOrWhiteSpace(Courriel))
            {
                Message = "svp remplisser tout les champs!";
                return;
            }

            bool isExistant = AuthRepository.CourrielIsExist(Courriel);

            if(isExistant && Courriel != _oldUtilisateur.Courriel)
            {
                Message = "ce courriel est déjà pris";
                return;
            }

            _oldUtilisateur.NomComplet = nomComplet;
            _oldUtilisateur.Courriel = Courriel;

            AuthRepository.UpdateUtilisateur(_oldUtilisateur);

            Message = "Le profil est à réussi a être mis à jour";
        }
    }
}