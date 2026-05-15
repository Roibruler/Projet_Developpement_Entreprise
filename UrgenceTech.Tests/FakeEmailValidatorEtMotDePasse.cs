namespace UrgenceTech.Tests
{
    public class FakeEmailValidatorEtMotDePasse
    {
        public (bool isValid, string message) IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return (false, "Le champ Courriel est obligatoire.");

            if (!email.Contains("@") || !email.Contains("."))
                return (false, "Format de courriel invalide.");

            return (true, null);
        }

        public string ConfiramtionMotDePasse(string motDePasse, string confirmation)
        {
            if (string.IsNullOrWhiteSpace(motDePasse))
                return "Le champ Mot de passe est obligatoire.";

            if (string.IsNullOrWhiteSpace(confirmation))
                return "Le champ Confirmer est obligatoire.";

            if (motDePasse != confirmation)
                return "Les mots de passe ne correspondent pas.";

            return null;
        }
    }
}