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
            modelBuilder.Entity<Utilisateur>().HasData(
                new Utilisateur
                {
                    ID = 1,
                    NomComplet = "Admin Test",
                    Courriel = "admin@urgencetech.com",
                    MotDePasse = "admin123",
                    Role = "Administrateur",
                    Status = true
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