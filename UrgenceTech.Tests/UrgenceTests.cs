using UrgenceTech.Models;
using UrgenceTech.Tests.Fakes;

namespace UrgenceTech.Tests
{
    public class UrgenceTests
    {
        [Fact]
        public void Urgence_SansTitre_EstInvalide()
        {
            var urgence = new Urgence { Titre = null, Priorite = "Haute" };

            var (estValide, messageErreur) = urgence.EstValide();

            Assert.False(estValide);
            Assert.Equal("Le titre est obligatoire.", messageErreur);
        }

        [Fact]
        public void Urgence_TitreVide_EstInvalide()
        {
            var urgence = new Urgence { Titre = "", Priorite = "Haute" };

            var (estValide, messageErreur) = urgence.EstValide();

            Assert.False(estValide);
            Assert.Equal("Le titre est obligatoire.", messageErreur);
        }

        [Fact]
        public void Urgence_SansPriorite_EstInvalide()
        {
            var urgence = new Urgence { Titre = "Urgence test", Priorite = "" };

            var (estValide, messageErreur) = urgence.EstValide();

            Assert.False(estValide);
            Assert.Equal("La priorité est obligatoire.", messageErreur);
        }

        [Fact]
        public void Urgence_Valide_EstAcceptee()
        {
            var urgence = new Urgence { Titre = "Urgence test", Priorite = "Haute" };

            var (estValide, messageErreur) = urgence.EstValide();

            Assert.True(estValide);
            Assert.Equal(string.Empty, messageErreur);
        }

    }

    public class CompteurUrgencesTests
    {
        private readonly FakeUrgenceRepository _repo;

        public CompteurUrgencesTests()
        {
            _repo = new FakeUrgenceRepository();
        }

        [Fact]
        public void CreerUrgence_CompteurIncremente()
        {
            var avant = _repo.ObtenirUrgencesEnCours().Count();

            _repo.CreerUrgence("Urgence test", "Description", "Haute", 1);

            var apres = _repo.ObtenirUrgencesEnCours().Count();
            Assert.Equal(avant + 1, apres);
        }

        [Fact]
        public void ResoudreUrgence_CompteurDecremente()
        {
            var urgence = _repo.CreerUrgence("Urgence test", "Description", "Haute", 1);

            _repo.MettreAJourStatut(urgence!.ID, "Résolue");

            var actives = _repo.ObtenirUrgencesEnCours().Count();
            Assert.Equal(0, actives);
        }

        [Fact]
        public void UrgenceResolue_NonCompteeDansActives()
        {
            _repo.CreerUrgence("Urgence 1", "Description", "Haute", 1);
            var urgence2 = _repo.CreerUrgence("Urgence 2", "Description", "Moyenne", 1);

            _repo.MettreAJourStatut(urgence2!.ID, "Résolue");

            var actives = _repo.ObtenirUrgencesEnCours().Count();
            Assert.Equal(1, actives); 
        }
    }
}