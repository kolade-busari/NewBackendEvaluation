using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace IntegraAdmin.Core.Resources
{
    public class SaveCustomerResource
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }
        public string Country { get; set; }

        public SponsorResource Sponsor { get; set; }
        public int SponsorId { get; set; }

        public ICollection<int> Products { get; set; }

        public SaveCustomerResource()
        {
            Products = new Collection<int>();
        }
    }
}
