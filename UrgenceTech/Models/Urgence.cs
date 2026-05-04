using System;
using System.ComponentModel.DataAnnotations;

namespace UrgenceTech.Models
{
    public class Urgence
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(200)]
        public string? Titre { get; set; }

        public string? Description { get; set; }

        [Required]
        public string? Priorite { get; set; }

        [Required]
        public string? Statut { get; set; } = "Ouverte";

        public DateTime DateCreation { get; set; } = DateTime.Now;

        public int UtilisateurID { get; set; }
        public Utilisateur? Utilisateur { get; set; }
    }
}