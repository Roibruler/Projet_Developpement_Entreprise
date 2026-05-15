using UrgenceTech.Models;

namespace UrgenceTech.Tests.ClassTestable
{
    // Le code vient de cette class sont dans le SignIn.xaml.cs
    public class ClassTestableVerrouillage
    {
        private const int dureeVerrouillage = 15;

        public bool EstVerrouille(Utilisateur utilisateur, DateTime maintenant)
        {
            if (!utilisateur.DateVerrouillage.HasValue)
                return false;

            var tempsRestant = utilisateur.DateVerrouillage.Value
                .AddMinutes(dureeVerrouillage) - maintenant;

            if (tempsRestant.TotalMinutes > 0)
                return true;

            utilisateur.DateVerrouillage = null;
            utilisateur.TentativesEchouees = 0;

            return false;
        }



    }
}
