using System;
using Microsoft.EntityFrameworkCore;
using UrgenceTech.Models;

namespace UrgenceTech.Data
{
    internal class AppDbContext : DbContext
    {
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Urgence> Urgences { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=urgencetech.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string motDePasseHash = BCrypt.Net.BCrypt.HashPassword("admin123");

            modelBuilder.Entity<Utilisateur>().HasData(
                new Utilisateur
                {
                    ID = 1,
                    NomComplet = "Admin Test",
                    Courriel = "admin@urgencetech.com",
                    MotDePasse = motDePasseHash,
                    Role = "Administrateur",
                    Status = true
                }
            );

            modelBuilder.Entity<Urgence>().HasData(
                new Urgence
                {
                    ID = 1,
                    Titre = "Patient en arrêt cardiaque",
                    Description = "Patient de 65 ans",
                    Priorite = "Critique",
                    Statut = "Ouverte",
                    DateCreation = new DateTime(2026, 5, 3),
                    UtilisateurID = 1
                },
                new Urgence
                {
                    ID = 2,
                    Titre = "Fracture du bras",
                    Description = "Patient de 25 ans",
                    Priorite = "Moyenne",
                    Statut = "En cours",
                    DateCreation = new DateTime(2026, 5, 3),
                    UtilisateurID = 1
                },
                new Urgence
                {
                    ID = 3,
                    Titre = "Allergie alimentaire",
                    Description = "Patient de 10 ans",
                    Priorite = "Haute",
                    Statut = "Résolue",
                    DateCreation = new DateTime(2026, 5, 3),
                    UtilisateurID = 1
                }
            );
        }
    }
}