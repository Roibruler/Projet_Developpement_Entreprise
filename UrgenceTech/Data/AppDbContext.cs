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
        }
    }
}