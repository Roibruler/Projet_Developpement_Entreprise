using UrgenceTech.Tests.Fakes;

namespace UrgenceTech.Tests.ClassTestable
{
    // Le code vient de cette class sont dans le SignUp.xaml.cs
    public class ClasseTestableEmailEtPassWord
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