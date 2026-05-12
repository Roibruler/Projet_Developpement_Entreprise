using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using UrgenceTech.Data;
using UrgenceTech.Models;
using System.Windows.Threading;

namespace UrgenceTech.ViewModels
{
    internal partial class TableauDeBordViewModel : ObservableObject
    {
        [ObservableProperty]
        private int nombreUrgencesActives;

        [ObservableProperty]
        private int nombreUrgencesResoluesAujourdhui;

        [ObservableProperty]
        private int nombreUtilisateursConnectes;

        [ObservableProperty]
        private ObservableCollection<Urgence> urgencesRecentes = new();

        [ObservableProperty]
        private bool estEnChargement = false;

        private readonly DispatcherTimer _timer;

        public TableauDeBordViewModel()
        {
            _ = ChargerDonneesAsync();
            // Timer qui rafraîchit les données toutes les 60 secondes
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(60);
            _timer.Tick += async (s, e) => await ChargerDonneesAsync();
            _timer.Start();
        }

        [RelayCommand]
        private async Task ChargerDonnees()
        {
            await ChargerDonneesAsync();
        }

        private async Task ChargerDonneesAsync()
        {
            EstEnChargement = true;

            try
            {
                using var context = new AppDbContext();

                NombreUrgencesActives = await context.Urgences
                    .CountAsync(u => u.Statut != "Résolue");

                NombreUrgencesResoluesAujourdhui = await context.Urgences
                    .CountAsync(u => u.Statut == "Résolue"
                        && u.DateCreation.Date == DateTime.Today);

                NombreUtilisateursConnectes = await context.Utilisateurs
                    .CountAsync(u => u.Status == true);

                var recentes = await context.Urgences
                    .Include(u => u.Utilisateur)
                    .Where(u => u.Statut != "Résolue")
                    .OrderByDescending(u => u.DateCreation)
                    .Take(10)
                    .ToListAsync();

                UrgencesRecentes.Clear();
                foreach (var urgence in recentes)
                    UrgencesRecentes.Add(urgence);
            }
            finally
            {
                EstEnChargement = false;
            }
        }
    }
}