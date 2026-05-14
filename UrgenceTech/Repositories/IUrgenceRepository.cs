using System.Collections.Generic;
using UrgenceTech.Models;

namespace UrgenceTech.Repositories
{

    public interface IUrgenceRepository
    {

        Urgence? CreerUrgence(string titre, string description, string priorite, int utilisateurId);

        IEnumerable<Urgence> ObtenirUrgencesEnCours();
        IEnumerable<Urgence> ObtenirToutesUrgences();
        Urgence? ObtenirParId(int id);

        bool MettreAJourStatut(int urgenceId, string nouveauStatut);
        bool AnnulerUrgence(int urgenceId);
    }
}
