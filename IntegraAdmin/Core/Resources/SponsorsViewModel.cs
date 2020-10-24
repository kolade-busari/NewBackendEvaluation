using IntegraAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegraAdmin.Core.Resources
{
    public class SponsorsViewModel
    {
        public List<ReadUserResource> SponsorUsers { get; set; }
        public List<Sponsor> Sponsors { get; set; }
    }
}
