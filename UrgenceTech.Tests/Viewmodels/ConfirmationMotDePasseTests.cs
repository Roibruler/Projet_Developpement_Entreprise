using Moq;

namespace UrgenceTech.Tests.ViewModels
{
    public interface IValidateurMotDePasse
    {
        bool ConfirmerMotDePasse(string motDePasse, string confirmation);
    }

    public class ValidateurMotDePasse : IValidateurMotDePasse
    {
        public bool ConfirmerMotDePasse(string motDePasse, string confirmation)
        {
            if (string.IsNullOrEmpty(confirmation))
                return false;

            return motDePasse == confirmation;
        }
    }

    public class ConfirmationMotDePasseTests
    {
        private readonly ValidateurMotDePasse _validateur;

        public ConfirmationMotDePasseTests()
        {
            _validateur = new ValidateurMotDePasse();
        }

        // deux meme mot de passe
        [Theory]
        [InlineData("motdepasse", "motdepasse")]
        [InlineData("P@ssw0rd!", "P@ssw0rd!")]
        [InlineData("abc12345", "abc12345")]
        public void ConfirmerMotDePasse_MotsDePasseIdentiques_RetourneVrai(
            string motDePasse, string confirmation)
        {
            // Arrange 

            // Act
            bool resultat = _validateur.ConfirmerMotDePasse(motDePasse, confirmation);

            // Assert
            Assert.True(resultat);
        }

        // mot de passe different
        [Theory]
        [InlineData("motdepasse", "autremotdepasse")]
        [InlineData("abc12345", "ABC12345")]
        public void ConfirmerMotDePasse_MotsDePasseDifferents_RetourneFaux(
            string motDePasse, string confirmation)
        {
            // Arrange 

            // Act
            bool resultat = _validateur.ConfirmerMotDePasse(motDePasse, confirmation);

            // Assert
            Assert.False(resultat);
        }

        // confirmation vide du champ mot de passe
        [Fact]
        public void ConfirmerMotDePasse_ConfirmationVide_RetourneFaux()
        {
            // Arrange

            // Act
            bool resultat = _validateur.ConfirmerMotDePasse("motdepasse", string.Empty);

            // Assert
            Assert.False(resultat);
        }

        [Fact]
        public void ConfirmerMotDePasse_ViaMock_MotsDePasseIdentiques_RetourneVrai()
        {
            // Arrange
            var mockValidateur = new Mock<IValidateurMotDePasse>();
            mockValidateur
                .Setup(v => v.ConfirmerMotDePasse("motdepasse", "motdepasse"))
                .Returns(true);

            // Act
            bool resultat = mockValidateur.Object.ConfirmerMotDePasse("motdepasse", "motdepasse");

            // Assert
            Assert.True(resultat);
        }
    }
}