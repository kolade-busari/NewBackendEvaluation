using IntegraAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegraAdmin.Core.Interfaces
{
    public interface ISponsorRepository
    {
        Task<List<Sponsor>> AllSponsors();
        Task<Sponsor> GetSponsor(int id);
        void AddSponsor(Sponsor sponsor);
        void RemoveSponsor(Sponsor sponsor);
    }
}
