using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrgenceTech.Models;

namespace UrgenceTech.Repositories
{
    internal interface IUrgenceRepository
    {
        Task<List<Urgence>> ObtenirToutesAsync();

        
        Task<List<Urgence>> ObtenirParStatutAsync(string statut);
        Task<List<Urgence>> ObtenirParPrioriteAsync(string priorite);
    }
}
