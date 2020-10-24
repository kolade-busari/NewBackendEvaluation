using IntegraAdmin.Core.Interfaces;
using IntegraAdmin.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegraAdmin.Persistence
{
    public class SponsorRepository : ISponsorRepository
    {
        private readonly ApplicationDbContext _context;

        public SponsorRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddSponsor(Sponsor sponsor)
        {
            _context.Sponsors.AddAsync(sponsor);
        }

        public async Task<List<Sponsor>> AllSponsors()
        {
            return await _context.Sponsors.ToListAsync();
        }

        public async Task<Sponsor> GetSponsor(int id)
        {
            return await _context.Sponsors.FindAsync(id);
        }

        public void RemoveSponsor(Sponsor sponsor)
        {
            _context.Sponsors.Remove(sponsor);
        }
    }
}
