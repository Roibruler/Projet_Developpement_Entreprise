using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrgenceTech.Models
{

    public class Urgence
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(200)]
        public string? Titre { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string Statut { get; set; } = "En attente";

        [MaxLength(20)]
        public string Priorite { get; set; } = "Moyen";
        public DateTime DateCreation { get; set; } = DateTime.Now;
        public DateTime? DateMiseAJour { get; set; }

        public int UtilisateurID { get; set; }

        [ForeignKey(nameof(UtilisateurID))]
        public Utilisateur? Utilisateur { get; set; }
        public int? TechnicienAssigneID { get; set; }

        [ForeignKey(nameof(TechnicienAssigneID))]
        public Utilisateur? TechnicienAssigne { get; set; }
    }
}