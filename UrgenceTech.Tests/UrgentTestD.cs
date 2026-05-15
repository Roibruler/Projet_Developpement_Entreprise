
using UrgenceTech.Models;
using UrgenceTech.Repositories;
using UrgenceTech.Tests.Fakes;
using UrgenceTech.ViewModels;
using Microsoft.EntityFrameworkCore;
using UrgenceTech.Repositories;
using UrgenceTech.Data;

namespace UrgenceTech.Tests
{
    public class UrgentTestD
    {



        [Theory]
        [InlineData("", false, "Le champ Courriel est obligatoire.")]
        [InlineData("TestEmail.com", false,"Format de courriel invalide.")]
        [InlineData("TestEmail@emailcom", false, "Format de courriel invalide.")]
        [InlineData("TestEmail@email.com", true, null)]
        public void Courriel_Sans_Arobase_ValidationEchoue(string email, bool expectedResults, string expectedMessage)
        {

            // Arrange — préparer

            var motCritère = new ClasseTestableEmailEtPassWord();

            // Act — exécuter

            var result = motCritère.IsValidEmail(email);


            // Assert — vérifier
            Assert.Equal(expectedResults, result.isValid);
            Assert.Equal(expectedMessage, result.message);

        }

        [Fact]
        public void MotDePasse_Cinq_Caractère_ValidationEchoue()
        {
            // Arrange — préparer
            var motCritère = new CritèreViewModel();
            string password = "Pa@1";

            // Act — exécuter
            motCritère.MotDePasse = password;

            // Assert — vérifier
            Assert.False(motCritère.MinLength);
            Assert.True(motCritère.SpecialCharactère);
            Assert.True(motCritère.Majuscule);
            Assert.True(motCritère.Minuscule);
            Assert.False(motCritère.IsPasswordValid);


        }

        [Fact]
        public void MotDePasse_Huit_Caractère_ValidationReussit()
        {
            // Arrange — préparer
            var motCritère = new CritèreViewModel();
            string password = "Bob@1234";

            // Act — exécuter
            motCritère.MotDePasse = password;

            // Assert — vérifier
            Assert.True(motCritère.MinLength);
            Assert.True(motCritère.SpecialCharactère);
            Assert.True(motCritère.Majuscule);
            Assert.True(motCritère.Minuscule);
            Assert.True(motCritère.IsPasswordValid);
        }

        [Theory]
        [InlineData("", "Bob", "Le champ Mot de passe est obligatoire.")]
        [InlineData("Bob", "", "Le champ Confirmer est obligatoire.")]
        [InlineData("Bob", "Bob1", "Les mots de passe ne correspondent pas.")]
        [InlineData("Bob", "Bob", null)]
        public void MotDePasse_Deux_Different_ValidationEchoue(string motDePasse, string confirmation, string expectedMessage)
        {
            // Arrange — préparer
            var motDePasseConfirmation = new ClasseTestableEmailEtPassWord();

            // Act — exécuter
            var resultat = motDePasseConfirmation.ConfiramtionMotDePasse(motDePasse, confirmation);

            // Assert — vérifier
            Assert.Equal(expectedMessage, resultat);

        }
    }


    public class DeverrouillageAutomatique()
    {




        [Fact]
        public void Verrouillage_Compte_Cinq_Mins()
        {

            // Arrange — préparer
            var service = new ClassTestableVerrouillage();
            var utilisateur = new Utilisateur
            {
                DateVerrouillage = DateTime.Now.AddMinutes(-5),
            };

            // Act — exécuter

            bool resultat = service.EstVerrouille(utilisateur, DateTime.Now);

            // Assert — vérifier
            //il est true qu'il a du temps restant
            Assert.True(resultat);

        }

        [Fact]
        public void Verrouillage_Compte_Seize_Mins()
        {

            // Arrange — préparer
            var service = new ClassTestableVerrouillage();
            var utilisateur = new Utilisateur
            {
                DateVerrouillage = DateTime.Now.AddMinutes(-16),
            };
            // Act — exécuter
            bool resultat = service.EstVerrouille(utilisateur, DateTime.Now);

            // Assert — vérifier
            //il est false, il ne reste plus de temps restant
            Assert.False(resultat);


        }

        [Fact]
        public void Verifier_Deverrouillage_Tentative_Egale_Zero()
        {

            // Arrange — préparer
            var service = new ClassTestableVerrouillage();
            var utilisateur = new Utilisateur
            {
                DateVerrouillage = DateTime.Now.AddMinutes(-16),
                TentativesEchouees = 5
            };
            // Act — exécuter
            bool resultat = service.EstVerrouille(utilisateur, DateTime.Now);

            // Assert — vérifier
            //il est false, il ne reste plus de temps restant
            Assert.False(resultat);
            Assert.Equal(0, utilisateur.TentativesEchouees);

        }

     
    }

}
