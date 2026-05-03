using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using UrgenceTech.Data;
using UrgenceTech.Models;

namespace UrgenceTech.ViewModels
{
    internal partial class TableauDeBordViewModel : ObservableObject
    {
        [ObservableProperty]
        private int nombreUrgencesActives;

        [ObservableProperty]
        private ObservableCollection<Urgence> urgencesRecentes = new();

        [ObservableProperty]
        private bool estEnChargement = false;

        public TableauDeBordViewModel()
        {
            _ = ChargerDonneesAsync();
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