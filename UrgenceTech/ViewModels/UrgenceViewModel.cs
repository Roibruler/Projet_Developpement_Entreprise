using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using UrgenceTech.Data;
using UrgenceTech.Models;
using UrgenceTech.Repositories;

namespace UrgenceTech.ViewModels
{
    internal partial class UrgenceViewModel : ObservableObject
    {
        private readonly IUrgenceRepository _repository;

        private List<Urgence> _toutesLesUrgences = new();

        [ObservableProperty]
        private ObservableCollection<Urgence> urgencesFiltrees = new();

        [ObservableProperty]
        private string filtreStatut = "Tous";

        [ObservableProperty]
        private string filtrePriorite = "Tous";

        [ObservableProperty]
        private string texteRecherche = string.Empty;

        public UrgenceViewModel()
        {
            _repository = new UrgenceRepository(new AppDbContext());
            _ = ChargerUrgencesAsync();
        }

        private async Task ChargerUrgencesAsync()
        {
            _toutesLesUrgences = await _repository.ObtenirToutesAsync();
            AppliquerFiltres();
        }

        private void AppliquerFiltres() 
        {
            var resultat = _toutesLesUrgences.AsEnumerable();

            
            if (FiltreStatut != "Tous")
                resultat = resultat.Where(u => u.Statut == FiltreStatut);

            
            if (FiltrePriorite != "Tous")
                resultat = resultat.Where(u => u.Priorite == FiltrePriorite);

            
            if (!string.IsNullOrWhiteSpace(TexteRecherche))
                resultat = resultat.Where(u =>
                    (u.Titre != null && u.Titre.Contains(TexteRecherche, StringComparison.OrdinalIgnoreCase)) ||
                    u.ID.ToString().Contains(TexteRecherche));

            UrgencesFiltrees = new ObservableCollection<Urgence>(resultat);
        }

        partial void OnFiltreStatutChanged(string value) => AppliquerFiltres();
        partial void OnFiltrePrioriteChanged(string value) => AppliquerFiltres();
        partial void OnTexteRechercheChanged(string value) => AppliquerFiltres();

    }
}
