using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UrgenceTech.Data;
using UrgenceTech.Models;

namespace UrgenceTech
{
    internal class AuthService
    {
        private const int MaxTentatives = 5;
        private const int DureeVerrouillageMinutes = 15;

        public static string HasherMotDePasse(string motDePasse)
        {
            return BCrypt.Net.BCrypt.HashPassword(motDePasse);
        }

        public static bool VerifierMotDePasse(string motDePasse, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(motDePasse, hash);
        }

        public static async Task<(Utilisateur? utilisateur, string messageErreur)> SeConnecterAsync(string courriel, string motDePasse)
        {
            using var context = new AppDbContext();

            var utilisateur = await context.Utilisateurs
                .FirstOrDefaultAsync(u => u.Courriel == courriel && u.Status == true);

            if (utilisateur == null)
                return (null, "Identifiants incorrects.");

            if (utilisateur.DateVerrouillage.HasValue)
            {
                var tempsRestant = utilisateur.DateVerrouillage.Value.AddMinutes(DureeVerrouillageMinutes) - DateTime.Now;

                if (tempsRestant > TimeSpan.Zero)
                {
                    int minutesRestantes = (int)Math.Ceiling(tempsRestant.TotalMinutes);
                    return (null, $"Compte verrouillé. Réessayez dans {minutesRestantes} minute(s).");
                }

                utilisateur.DateVerrouillage = null;
                utilisateur.TentativesEchouees = 0;
            }

            if (!VerifierMotDePasse(motDePasse, utilisateur.MotDePasse!))
            {
                utilisateur.TentativesEchouees++;

                if (utilisateur.TentativesEchouees >= MaxTentatives)
                {
                    utilisateur.DateVerrouillage = DateTime.Now;
                    await context.SaveChangesAsync();
                    return (null, $"Compte verrouillé après {MaxTentatives} tentatives échouées. Réessayez dans {DureeVerrouillageMinutes} minutes.");
                }

                int tentativesRestantes = MaxTentatives - utilisateur.TentativesEchouees;
                await context.SaveChangesAsync();
                return (null, $"Identifiants incorrects. {tentativesRestantes} tentative(s) restante(s).");
            }

            utilisateur.TentativesEchouees = 0;
            utilisateur.DateVerrouillage = null;
            await context.SaveChangesAsync();

            return (utilisateur, string.Empty);
        }

        public static async Task<(bool succes, string messageErreur)> CreerCompteAsync(string nomComplet, string courriel, string motDePasse)
        {
            using var context = new AppDbContext();

            bool courrielExiste = await context.Utilisateurs
                .AnyAsync(u => u.Courriel == courriel);

            if (courrielExiste)
                return (false, "Ce courriel est déjà utilisé. Veuillez vous connecter.");

            var nouvelUtilisateur = new Utilisateur
            {
                NomComplet = nomComplet,
                Courriel = courriel,
                MotDePasse = HasherMotDePasse(motDePasse),
                Role = "Médecin",
                Status = true
            };

            context.Utilisateurs.Add(nouvelUtilisateur);
            await context.SaveChangesAsync();

            return (true, string.Empty);
        }
    }
}