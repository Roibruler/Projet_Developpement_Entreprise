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
    public class VoirUtilisateurViewModel : INotifyPropertyChanged
    {

        private readonly AppDbContext context;

        private string search;
        public string Search
        {
            get => search;
            set
            {
                search = value;
                OnPropertyChanged();
                FiltreEtTrier();
            }
        }

        public ObservableCollection<Utilisateur> Utilisateurs {  get; set; }

        private string sortColumn = "NomComplet";

        public VoirUtilisateurViewModel(AppDbContext context)
        {
            this.context = context;
            Utilisateurs = new ObservableCollection<Utilisateur>();
            LoadUsers();
        }

        //les met dans la liste avec leur nom, etc
        private void LoadUsers()
        {
            Utilisateurs.Clear();
            foreach (var utilisateur in context.Utilisateurs )
                Utilisateurs.Add(utilisateur);
        }
        //trier par column
        public void Trier(string column)
        {
            sortColumn = column;
            FiltreEtTrier();
        }


        //
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

            Utilisateurs.Clear();
            foreach (var user in query)
                Utilisateurs.Add(user);


        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = null)
                   => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

    }
}
