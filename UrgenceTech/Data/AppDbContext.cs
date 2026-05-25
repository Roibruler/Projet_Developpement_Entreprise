using Microsoft.EntityFrameworkCore;
using System.IO;
using UrgenceTech.Models;

namespace UrgenceTech.Data
{
    internal class AppDbContext : DbContext
    {
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Urgence> Urgences { get; set; }

        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "urgencetech.db");
                options.UseSqlite($"Data Source={dbPath}");
            }
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
                    Status = true,
                    TentativesEchouees = 0,
                    DateCreation = DateTime.Now,
                }
            );

            modelBuilder.Entity<Urgence>()
                .HasOne(u => u.Utilisateur)
                .WithMany()
                .HasForeignKey(u => u.UtilisateurID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Urgence>()
                .HasOne(u => u.TechnicienAssigne)
                .WithMany()
                .HasForeignKey(u => u.TechnicienAssigneID)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);
        }
    }
}