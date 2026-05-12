using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UrgenceTech.Data;
using UrgenceTech.Models;

namespace UrgenceTech.ViewModels
{
    internal partial class VoirUtilisateurViewModel : ObservableObject
    {

        private readonly AppDbContext context;

        [ObservableProperty]
        private string search;

        [ObservableProperty]
        private ObservableCollection<Utilisateur> utilisateurs;

        private string sortColumn = "NomComplet";


        public VoirUtilisateurViewModel(AppDbContext context)
        {
            this.context = context;
            utilisateurs = new ObservableCollection<Utilisateur>();
            LoadUsers();
        }



        partial void OnSearchChanged(string value)
        {
            FiltreEtTrier();
        }


        //les met dans la liste avec leur nom, etc
        private void LoadUsers()
        {
            utilisateurs.Clear();
            foreach (var utilisateur in context.Utilisateurs )
                utilisateurs.Add(utilisateur);
        }
        //trier par column
        public void Trier(string column)
        {
            sortColumn = column;
            FiltreEtTrier();
        }


        
        private void FiltreEtTrier()
        {
            //prend les utilisateur
            var query = context.Utilisateurs.AsQueryable();

            //Search bar
            if (!string.IsNullOrWhiteSpace(Search))
            {
                query = query.Where(u =>
                    u.NomComplet.Contains(Search) ||
                    u.Courriel.Contains(Search));
            }

            //trie les utilisateurs
            query = sortColumn switch
            {
                "Role" => query.OrderBy(u => u.Role),
                "Status" => query.OrderBy(u => u.Status),
                _ => query.OrderBy(u => u.NomComplet)
            };

            utilisateurs.Clear();
            foreach (var user in query)
                utilisateurs.Add(user);


        }

    }
}
