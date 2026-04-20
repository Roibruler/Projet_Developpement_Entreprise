using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UrgenceTech.Models;

namespace UrgenceTech.Data
{
    internal class AppDbContext : DbContext
    {
        public DbSet<Utilisateur> Utilisateurs { get; set; }

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
        }

    }

}
