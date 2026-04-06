using System;
using System.Linq;
using UrgenceTech.Models;

namespace UrgenceTech
{
    public class AuthService
    {
        public static User UtilisateurConnecte { get; private set; }

        /// Crée un nouveau compte patient dans la base de données.
        /// Retourne l'utilisateur créé ou null si le courriel existe déjà.
        public static User CreerCompte(string nom, string prenom, string courriel, string motDePasse)
        {
            using var context = new Data.UrgenceTechContext();

            // Vérifier si le courriel est déjà utilisé
            bool courrielExiste = context.Utilisateurs
                .Any(u => u.Courriel.ToLower() == courriel.ToLower());

            if (courrielExiste)
                return null;

            var nouvelUtilisateur = new User
            {
                Nom = nom.Trim(),
                Prenom = prenom.Trim(),
                Courriel = courriel.Trim().ToLower(),
                MotDePasse = motDePasse,
                Role = "Patient",
                DateCreation = DateTime.Now,
                EstActif = true
            };

            context.Utilisateurs.Add(nouvelUtilisateur);
            context.SaveChanges();

            // Connecter automatiquement après la création
            UtilisateurConnecte = nouvelUtilisateur;
            return nouvelUtilisateur;
        }

        public static bool SeConnecter(string courriel, string motDePasse)
        {
            using var context = new Data.UrgenceTechContext();

            var utilisateur = context.Utilisateurs
                .FirstOrDefault(u =>
                    u.Courriel.ToLower() == courriel.ToLower() &&
                    u.MotDePasse == motDePasse &&
                    u.EstActif);

            if (utilisateur == null) return false;

            UtilisateurConnecte = utilisateur;
            return true;
        }

        public static void SeDeconnecter()
        {
            UtilisateurConnecte = null;
        }
    }
}