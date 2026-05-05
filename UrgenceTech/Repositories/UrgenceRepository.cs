using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UrgenceTech.Data;
using UrgenceTech.Models;

namespace UrgenceTech.Repositories
{
    internal class UrgenceRepository : IUrgenceRepository
    {
        private readonly AppDbContext _context;

        public UrgenceRepository(AppDbContext context)
        {
            _context = context;
        }

        // Urgences avec leur utilisateur
        public async Task<List<Urgence>> ObtenirToutesAsync()
        {
            return await _context.Urgences
                .Include(u => u.Utilisateur)
                .OrderByDescending(u => u.DateCreation)
                .ToListAsync();
        }

        // Urgences filtrées par statut
        public async Task<List<Urgence>> ObtenirParStatutAsync(string statut)
        {
            return await _context.Urgences
                .Include(u => u.Utilisateur)
                .Where(u => u.Statut == statut)
                .OrderByDescending(u => u.DateCreation)
                .ToListAsync();
        }

        // Urgences filtrées par priorité
        public async Task<List<Urgence>> ObtenirParPrioriteAsync(string priorite)
        {
            return await _context.Urgences
                .Include(u => u.Utilisateur)
                .Where(u => u.Priorite == priorite)
                .OrderByDescending(u => u.DateCreation)
                .ToListAsync();
        }

    }
}
