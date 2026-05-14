using System.Threading.Tasks;
using UrgenceTech.Models;
using UrgenceTech.Repositories;

namespace UrgenceTech
{
    public static class AuthService
    {
        private static IAuthRepository _repo = new AuthRepository();

        public static void SetRepository(IAuthRepository repo) => _repo = repo;

        public static Utilisateur? UtilisateurConnecte => _repo.UtilisateurConnecte;

        public static bool MotDePasseValide(string motDePasse)
            => _repo.MotDePasseValide(motDePasse);

        public static Utilisateur? CreerCompte(string nomComplet, string courriel, string motDePasse, string role = "Patient")
            => _repo.CreerCompte(nomComplet, courriel, motDePasse, role);

        public static bool SeConnecter(string courriel, string motDePasse, bool sesouvenir = false)
            => _repo.SeConnecter(courriel, motDePasse, sesouvenir);

        public static void SeDeconnecter(bool oublier = false)
            => _repo.SeDeconnecter(oublier);

        public static bool ChargerSession()
            => _repo.ChargerSession();

        public static Task<(Utilisateur? utilisateur, string erreur)> SeConnecterAsync(string courriel, string motDePasse, bool sesouvenir = false)
        {
            bool succes = _repo.SeConnecter(courriel, motDePasse, sesouvenir);

            if (succes)
                return Task.FromResult<(Utilisateur?, string)>((_repo.UtilisateurConnecte, string.Empty));

            return Task.FromResult<(Utilisateur?, string)>((null, "Courriel ou mot de passe invalide."));
        }

        public static Task<(bool succes, string erreur)> CreerCompteAsync(string nomComplet, string courriel, string motDePasse, string role = "Patient")
        {
            if (!_repo.MotDePasseValide(motDePasse))
                return Task.FromResult((false, "Le mot de passe ne respecte pas les criteres de securite."));

            var utilisateur = _repo.CreerCompte(nomComplet, courriel, motDePasse, role);

            if (utilisateur == null)
                return Task.FromResult((false, "Ce courriel est deja utilise."));

            return Task.FromResult((true, string.Empty));
        }
    }
}