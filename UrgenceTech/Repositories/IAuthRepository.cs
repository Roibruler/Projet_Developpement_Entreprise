using UrgenceTech.Models;

namespace UrgenceTech.Repositories
{

    public interface IAuthRepository
    {

        bool MotDePasseValide(string motDePasse);
        Utilisateur? CreerCompte(string nomComplet, string courriel, string motDePasse, string role = "Patient");
        bool SeConnecter(string courriel, string motDePasse, bool sesouvenir = false);
        void SeDeconnecter(bool oublier = false);
        bool ChargerSession();
        Utilisateur? UtilisateurConnecte { get; }
    }
}
