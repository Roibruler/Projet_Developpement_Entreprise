using UrgenceTech.Models;
using UrgenceTech.Repositories;

namespace UrgenceTech.Tests.Fakes
{
    public class FakeAuthRepository : IAuthRepository
    {
        private readonly List<Utilisateur> _utilisateurs = new();
        private Utilisateur? _utilisateurConnecte;
        private int _nextId = 1;

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

            bool courrielExiste = _utilisateurs
                .Any(u => u.Courriel!.ToLower() == courriel.ToLower());

            if (courrielExiste)
                return null;

            var nouvelUtilisateur = new Utilisateur
            {
                ID = _nextId++,
                NomComplet = nomComplet.Trim(),
                Courriel = courriel.Trim().ToLower(),
                MotDePasse = motDePasse,
                Role = role,
                Status = true
            };

            _utilisateurs.Add(nouvelUtilisateur);
            _utilisateurConnecte = nouvelUtilisateur;
            return nouvelUtilisateur;
        }

        public bool SeConnecter(string courriel, string motDePasse, bool sesouvenir = false)
        {
            var utilisateur = _utilisateurs
                .FirstOrDefault(u =>
                    u.Courriel!.ToLower() == courriel.ToLower() &&
                    u.MotDePasse == motDePasse &&
                    u.Status == true);

            if (utilisateur == null)
                return false;

            _utilisateurConnecte = utilisateur;
            return true;
        }

        public void SeDeconnecter(bool oublier = false)
        {
            _utilisateurConnecte = null;
        }

        public bool ChargerSession() => false;
    }
}