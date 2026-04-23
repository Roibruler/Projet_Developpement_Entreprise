using System;
using System.Linq;
using UrgenceTech.Data;
using UrgenceTech.Models;

namespace UrgenceTech
{
    public class AuthService
    {
        public static Utilisateur UtilisateurConnecte { get; private set; }

        

        /// Fonctionnalité 1 [Force et complexité du mot de passe]
        /// Min 8 caractères, 1 majuscule, 1 chiffre. Simple mais au besoin j'ajouterai si prof le demande 
        public static bool MotDePasseValide(string motDePasse)
        {
            if (string.IsNullOrWhiteSpace(motDePasse) || motDePasse.Length < 8)
                return false;

            return motDePasse.Any(char.IsUpper) && motDePasse.Any(char.IsDigit);
        }

        

        /// Retourne le nouvel Utilisateur, ou null si courriel déjà utilisé ou mot de passe invalide.
        public static Utilisateur CreerCompte(string nomComplet, string courriel, string motDePasse, string role = "Patient")
        {
            if (!MotDePasseValide(motDePasse))
                return null;

            using var context = new AppDbContext();

            bool courrielExiste = context.Utilisateurs
                .Any(u => u.Courriel.ToLower() == courriel.ToLower());

            if (courrielExiste)
                return null;

            var nouvelUtilisateur = new Utilisateur
            {
                NomComplet = nomComplet.Trim(),
                Courriel = courriel.Trim().ToLower(),
                MotDePasse = motDePasse,
                Role = role,
                Status = true
            };

            context.Utilisateurs.Add(nouvelUtilisateur);
            context.SaveChanges();

            UtilisateurConnecte = nouvelUtilisateur;
            return nouvelUtilisateur;
        }

       

        /// Retourne true si la connexion réussit.
        public static bool SeConnecter(string courriel, string motDePasse, bool sesouvenir = false)
        {
            using var context = new AppDbContext();

            var utilisateur = context.Utilisateurs
                .FirstOrDefault(u =>
                    u.Courriel.ToLower() == courriel.ToLower() &&
                    u.MotDePasse == motDePasse &&
                    u.Status == true);

            if (utilisateur == null)
                return false;

            UtilisateurConnecte = utilisateur;

            if (sesouvenir)
                SauvegarderSession(utilisateur.Courriel);

            return true;
        }

        //pour déconnecter

        public static void SeDeconnecter(bool oublier = false)
        {
            UtilisateurConnecte = null;

            if (oublier)
                SupprimerSession();
        }

        // Fonctionnalité 2 [Se souvenir de moi]
        // Dans le folder Properties je mets un fichier Setings.settings qui permet d'enregistrer la propriété CourrielSessoin c'est du WPF(persistance)

        private static void SauvegarderSession(string courriel)
        {
            Properties.Settings.Default.CourrielSession = courriel;
            Properties.Settings.Default.Save();
        }

        private static void SupprimerSession()
        {

            Properties.Settings.Default.CourrielSession = string.Empty;
            Properties.Settings.Default.Save();

        }

        public static bool ChargerSession()
        {
            string courriel = Properties.Settings.Default.CourrielSession;

            if (string.IsNullOrEmpty(courriel))
                return false;

                using var context = new AppDbContext();
                var utilisateur = context.Utilisateurs
                .FirstOrDefault(u => u.Courriel == courriel && u.Status);

            if (utilisateur == null)
                return false;

            UtilisateurConnecte = utilisateur;
            return true;
        }
    }
}