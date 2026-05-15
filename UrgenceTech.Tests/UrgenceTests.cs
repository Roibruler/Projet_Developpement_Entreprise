using UrgenceTech.Models;
using UrgenceTech.Repositories;
using UrgenceTech.Tests.Fakes;
using Xunit;

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

    public class AuthServiceTests
    {
        [Fact]
        public void HasherMotDePasse_RetourneHashValide()
        {
            string motDePasse = "Admin123";

            string hash = AuthService.HasherMotDePasse(motDePasse);

            Assert.NotNull(hash);
            Assert.NotEqual(motDePasse, hash);
            Assert.StartsWith("$2", hash);
        }

        [Fact]
        public void HasherMotDePasse_DeuxAppels_RetournentHashDifferents()
        {
            string motDePasse = "Admin123";

            string hash1 = AuthService.HasherMotDePasse(motDePasse);
            string hash2 = AuthService.HasherMotDePasse(motDePasse);

            Assert.NotEqual(hash1, hash2);
        }

        [Fact]
        public void VerifierMotDePasse_BonMotDePasse_RetourneTrue()
        {
            string motDePasse = "Admin123";
            string hash = AuthService.HasherMotDePasse(motDePasse);

            bool resultat = AuthService.VerifierMotDePasse(motDePasse, hash);

            Assert.True(resultat);
        }

        [Fact]
        public void VerifierMotDePasse_MauvaisMotDePasse_RetourneFalse()
        {
            string motDePasse = "Admin123";
            string hash = AuthService.HasherMotDePasse(motDePasse);

            bool resultat = AuthService.VerifierMotDePasse("mauvais", hash);

            Assert.False(resultat);
        }

        [Fact]
        public void VerifierMotDePasse_ChampVide_RetourneFalse()
        {
            string motDePasse = "Admin123";
            string hash = AuthService.HasherMotDePasse(motDePasse);

            bool resultat = AuthService.VerifierMotDePasse(string.Empty, hash);

            Assert.False(resultat);
        }
    }

    public class CourrielUniciteTests
    {
        private FakeAuthRepository CreerRepo() => new FakeAuthRepository();

        [Fact]
        public void CreerCompte_CourrielExistant_RetourneNull()
        {
            var repo = CreerRepo();
            AuthService.SetRepository(repo);

            AuthService.CreerCompte("User Un", "test@test.com", "Motdepasse1");

            var resultat = AuthService.CreerCompte("User Deux", "test@test.com", "Motdepasse1");

            Assert.Null(resultat);
        }

        [Fact]
        public void CreerCompte_CourrielUnique_ReussitCreation()
        {
            var repo = CreerRepo();
            AuthService.SetRepository(repo);

            var resultat = AuthService.CreerCompte("User Un", "unique@test.com", "Motdepasse1");

            Assert.NotNull(resultat);
        }

        [Fact]
        public void CreerCompteAsync_CourrielExistant_RetourneErreur()
        {
            var repo = CreerRepo();
            AuthService.SetRepository(repo);

            AuthService.CreerCompte("User Un", "test@test.com", "Motdepasse1");

            var (succes, message) = AuthService.CreerCompteAsync("User Deux", "test@test.com", "Motdepasse1").Result;

            Assert.False(succes);
            Assert.Equal("Ce courriel est deja utilise.", message);
        }
    }
}