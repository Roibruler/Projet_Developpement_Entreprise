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
    }
}