using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UrgenceTech.Models;
using UrgenceTech.Repositories;

namespace UrgenceTech.ViewModels
{

    public partial class UrgenceViewModel : ObservableObject
    {
        private readonly IUrgenceRepository _urgenceRepo;

        public UrgenceViewModel() : this(new UrgenceRepository()) { }

        public UrgenceViewModel(IUrgenceRepository urgenceRepo)
        {
            _urgenceRepo = urgenceRepo;
            ChargerUrgencesEnCours();
        }

        [ObservableProperty]
        private ObservableCollection<Urgence> urgencesEnCours = new();

        [ObservableProperty]
        private Urgence? urgenceSelectionnee;

        [ObservableProperty]
        private string messageVue = string.Empty;
        [RelayCommand]
        private void ChargerUrgencesEnCours()
        {
            UrgencesEnCours.Clear();
            MessageVue = string.Empty;

            var liste = _urgenceRepo.ObtenirUrgencesEnCours();

            foreach (var u in liste)
                UrgencesEnCours.Add(u);

            if (UrgencesEnCours.Count == 0)
                MessageVue = "Aucune urgence en cours pour le moment.";
        }

        [ObservableProperty]
        private string titre = string.Empty;

        [ObservableProperty]
        private string description = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreerUrgenceCommand))]
        private string prioriteSelectionnee = "Moyen";

        [ObservableProperty]
        private string messageCreation = string.Empty;

        [ObservableProperty]
        private bool creationReussie;
        public static IReadOnlyList<string> Priorites { get; } =
            new[] { "Faible", "Moyen", "Élevé", "Critique" };

        private bool PeutCreer() =>
            !string.IsNullOrWhiteSpace(Titre) &&
            AuthService.UtilisateurConnecte != null;

        [RelayCommand(CanExecute = nameof(PeutCreer))]
        private void CreerUrgence()
        {
            MessageCreation = string.Empty;
            CreationReussie = false;

            if (string.IsNullOrWhiteSpace(Titre))
            {
                MessageCreation = "Le titre est obligatoire.";
                return;
            }

            if (AuthService.UtilisateurConnecte == null)
            {
                MessageCreation = "Vous devez être connecté pour créer une urgence.";
                return;
            }

            var urgence = _urgenceRepo.CreerUrgence(
                titre: Titre.Trim(),
                description: Description.Trim(),
                priorite: PrioriteSelectionnee,
                utilisateurId: AuthService.UtilisateurConnecte.ID
            );

            if (urgence == null)
            {
                MessageCreation = "Erreur lors de la création. Veuillez réessayer.";
                return;
            }

            MessageCreation = $"Urgence « {urgence.Titre} » créée avec succès (#{urgence.ID}).";
            CreationReussie = true;
            Titre = string.Empty;
            Description = string.Empty;
            PrioriteSelectionnee = "Moyen";
        }
    }
}