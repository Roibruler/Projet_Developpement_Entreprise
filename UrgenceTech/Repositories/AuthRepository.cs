using System.Linq;
using UrgenceTech.Data;
using UrgenceTech.Models;

namespace UrgenceTech.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private Utilisateur? _utilisateurConnecte;

        public Utilisateur? UtilisateurConnecte => _utilisateurConnecte;

        public bool MotDePasseValide(string motDePasse)
        {
            if (string.IsNullOrWhiteSpace(motDePasse) || motDePasse.Length < 8)
                return false;

            return motDePasse.Any(char.IsUpper) && motDePasse.Any(char.IsDigit);




        }
        public Utilisateur? CreerCompte(string nomComplet, string courriel, string motDePasse, string role = "Patient")
        {
            if (!MotDePasseValide(motDePasse))
                return null;

            using var context = new AppDbContext();

            bool courrielExiste = context.Utilisateurs
                .Any(u => u.Courriel!.ToLower() == courriel.ToLower());

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

            _utilisateurConnecte = nouvelUtilisateur;
            return nouvelUtilisateur;
        }






        public bool SeConnecter(string courriel, string motDePasse, bool sesouvenir = false)
        {
            using var context = new AppDbContext();

            var utilisateur = context.Utilisateurs
                .FirstOrDefault(u =>
                    u.Courriel!.ToLower() == courriel.ToLower() &&
                    u.MotDePasse == motDePasse &&
                    u.Status == true);

            if (utilisateur == null)
                return false;

            _utilisateurConnecte = utilisateur;

            if (sesouvenir)
                SauvegarderSession(utilisateur.Courriel!);

            return true;
        }






        public void SeDeconnecter(bool oublier = false)
        {
            _utilisateurConnecte = null;

            if (oublier)
                SupprimerSession();
        }
        public bool ChargerSession()
        {
            string courriel = Properties.Settings.Default.CourrielSession;

            if (string.IsNullOrEmpty(courriel))
                return false;

            using var context = new AppDbContext();
            var utilisateur = context.Utilisateurs
                .FirstOrDefault(u => u.Courriel == courriel && u.Status);

            if (utilisateur == null)
                return false;

            _utilisateurConnecte = utilisateur;
            return true;
        }




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
    }
}