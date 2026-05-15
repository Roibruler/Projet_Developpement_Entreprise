using System;
using System.ComponentModel.DataAnnotations;

namespace UrgenceTech.Models
{
    public class Utilisateur
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(100)]
        public string? NomComplet { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Courriel { get; set; }

        [Required]
        public string? MotDePasse { get; set; }

        [Required]
        public string? Role { get; set; }

        public bool Status { get; set; } = true;

        public int TentativesEchouees { get; set; } = 0;

        public DateTime? DateVerrouillage { get; set; }

        public DateTime DateCreation { get; set; } = DateTime.Now;
    }
}