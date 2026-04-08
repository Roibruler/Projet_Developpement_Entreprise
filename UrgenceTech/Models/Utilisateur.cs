using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public bool Status { get; set; } = true;

        [Required]
        [MaxLength(100)]
        public string? Role { get; set; }
    }
}
