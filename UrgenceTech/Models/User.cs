using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrgenceTech.Models
{
    public class User
    {
        public int Id { get; set; }
<<<<<<< HEAD
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Courriel { get; set; }
        public string MotDePasse { get; set; }
        public string Role { get; set; } = "Patient";
=======
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? Courriel { get; set; }
        public string? MotDePasseHash { get; set; }
        public string? Role { get; set; } = "Patient";
>>>>>>> origin/main
        public DateTime DateCreation { get; set; } = DateTime.Now;
        public bool EstActif { get; set; } = true;
    }
}
