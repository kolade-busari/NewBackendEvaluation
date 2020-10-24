using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegraAdmin.Core.Resources
{
    public class ReadUserResource
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SponsorId { get; set; }
        public SponsorResource Sponsor { get; set; }
    }
}
