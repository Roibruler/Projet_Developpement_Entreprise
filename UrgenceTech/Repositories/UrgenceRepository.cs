using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UrgenceTech.Data;
using UrgenceTech.Models;

namespace UrgenceTech.Repositories
{
    public class UrgenceRepository : IUrgenceRepository
    {

        public static readonly string[] StatutsValides =
            { "En attente", "En cours", "Résolu", "Annulé" };
        public Urgence? CreerUrgence(string titre, string description, string priorite, int utilisateurId)
        {
            if (string.IsNullOrWhiteSpace(titre))
                return null;

            var urgence = new Urgence
            {
                Titre = titre.Trim(),
                Description = description?.Trim(),
                Priorite = priorite ?? "Moyen",
                Statut = "En attente",
                DateCreation = DateTime.Now,
                UtilisateurID = utilisateurId
            };

            using var context = new AppDbContext();
            context.Urgences.Add(urgence);
            context.SaveChanges();

            return urgence;
        }
        public IEnumerable<Urgence> ObtenirUrgencesEnCours()
        {
            using var context = new AppDbContext();
            return context.Urgences
                .Include(u => u.Utilisateur)
                .Include(u => u.TechnicienAssigne)
                .Where(u => u.Statut == "En cours")
                .OrderByDescending(u => u.DateCreation)
                .ToList();
        }

        public IEnumerable<Urgence> ObtenirToutesUrgences()
        {
            using var context = new AppDbContext();
            return context.Urgences
                .Include(u => u.Utilisateur)
                .Include(u => u.TechnicienAssigne)
                .OrderByDescending(u => u.DateCreation)
                .ToList();
        }

        public Urgence? ObtenirParId(int id)
        {
            using var context = new AppDbContext();
            return context.Urgences
                .Include(u => u.Utilisateur)
                .Include(u => u.TechnicienAssigne)
                .FirstOrDefault(u => u.ID == id);
        }
        public bool MettreAJourStatut(int urgenceId, string nouveauStatut)
        {
            if (!StatutsValides.Contains(nouveauStatut))
                return false;

            using var context = new AppDbContext();
            var urgence = context.Urgences.FirstOrDefault(u => u.ID == urgenceId);

            if (urgence == null)
                return false;

            urgence.Statut = nouveauStatut;
            urgence.DateMiseAJour = DateTime.Now;
            context.SaveChanges();

            return true;
        }
        public bool AnnulerUrgence(int urgenceId)
            => MettreAJourStatut(urgenceId, "Annulé");
    }
}