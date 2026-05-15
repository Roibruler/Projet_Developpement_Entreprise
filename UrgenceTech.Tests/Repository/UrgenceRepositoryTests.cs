using Moq;
using UrgenceTech.Models;
using UrgenceTech.Repositories;
using UrgenceTech.Tests.Fakes;

namespace UrgenceTech.Tests.Repository
{
    public class UrgenceRepositoryTests
    {
        private readonly FakeUrgenceRepository _repo;

        public UrgenceRepositoryTests()
        {
            _repo = new FakeUrgenceRepository();
        }

        // Si l'id est positif et superieur a 0.
        [Fact]
        public void CreerUrgence_UrgenceValide_IdPositifGenere()
        {
            // Arrange

            // Act
            Urgence? urgence = _repo.CreerUrgence("Panne serveur", "Serveur hors ligne", "Haute", 1);

            // Assert
            Assert.True(urgence!.ID > 0);
        }

        // Si deux urgences sont créées successivement, leurs IDs doivent être différents.
        [Fact]
        public void CreerUrgence_DeuxUrgencesSuccessives_IdsDifferents()
        {
            // Arrange

            // Act
            Urgence? urgence1 = _repo.CreerUrgence("Panne réseau", "Coupure LAN", "Haute", 1);
            Urgence? urgence2 = _repo.CreerUrgence("Coupure électrique", "Salle serveur", "Critique", 1);

            // Assert
            Assert.NotEqual(urgence1!.ID, urgence2!.ID);
        }

        // Lorsqu'une urgence est créée, son statut par défaut doit être "En attente".
        [Fact]
        public void CreerUrgence_UrgenceValide_StatutParDefautEstEnAttente()
        {
            // Arrange

            // Act
            Urgence? urgence = _repo.CreerUrgence("Disque plein", "Espace disque critique", "Moyenne", 1);

            // Assert
            Assert.Equal("En attente", urgence!.Statut);
        }

        // Le titre de l'urgence doit être correctement enregistré et conservé lors de la création.
        [Theory]
        [InlineData("Panne serveur")]
        [InlineData("Coupure réseau")]
        [InlineData("Mise à jour requise")]
        public void CreerUrgence_DiversTitres_TitreEstConserve(string titre)
        {
            // Arrange

            // Act
            Urgence? urgence = _repo.CreerUrgence(titre, "Description test", "Haute", 1);

            // Assert
            Assert.Equal(titre, urgence!.Titre);
        }

        // La priorité de l'urgence doit être correctement enregistrée et conservée lors de la création.
        [Theory]
        [InlineData("Haute")]
        [InlineData("Critique")]
        [InlineData("Basse")]
        public void CreerUrgence_DiversesPriorites_PrioriteEstConservee(string priorite)
        {
            // Arrange

            // Act
            Urgence? urgence = _repo.CreerUrgence("Titre test", "Description test", priorite, 1);

            // Assert
            Assert.Equal(priorite, urgence!.Priorite);
        }

        // Si le titre de l'urgence est vide, la création doit échouer et retourner null.
        [Fact]
        public void CreerUrgence_TitreVide_RetourneNull()
        {
            // Arrange

            // Act
            Urgence? urgence = _repo.CreerUrgence("", "Description test", "Haute", 1);

            // Assert
            Assert.Null(urgence);
        }

    }
}