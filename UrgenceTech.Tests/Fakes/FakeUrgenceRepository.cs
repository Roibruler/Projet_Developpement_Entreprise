using UrgenceTech.Models;
using UrgenceTech.Repositories;

namespace UrgenceTech.Tests.Fakes
{
    public class FakeUrgenceRepository : IUrgenceRepository
    {
        private List<Urgence> _urgences = new();
        private int _nextId = 1;

        public Urgence? CreerUrgence(string titre, string description, string priorite, int utilisateurId)
        {
            if (string.IsNullOrWhiteSpace(titre) || string.IsNullOrWhiteSpace(priorite))
                return null;

            var urgence = new Urgence
            {
                ID = _nextId++,
                Titre = titre,
                Description = description,
                Priorite = priorite,
                Statut = "En attente",
                UtilisateurID = utilisateurId
            };

            _urgences.Add(urgence);
            return urgence;
        }

        public IEnumerable<Urgence> ObtenirToutesUrgences()
        {
            return _urgences.ToList();
        }

        public IEnumerable<Urgence> ObtenirUrgencesEnCours()
        {
            return _urgences.Where(u => u.Statut != "Résolue").ToList();
        }

        public Urgence? ObtenirParId(int id)
        {
            return _urgences.FirstOrDefault(u => u.ID == id);
        }

        public bool MettreAJourStatut(int urgenceId, string nouveauStatut)
        {
            var urgence = _urgences.FirstOrDefault(u => u.ID == urgenceId);
            if (urgence == null) return false;

            urgence.Statut = nouveauStatut;
            return true;
        }

        public bool AnnulerUrgence(int urgenceId)
        {
            var urgence = _urgences.FirstOrDefault(u => u.ID == urgenceId);
            if (urgence == null) return false;

            _urgences.Remove(urgence);
            return true;
        }
    }
}