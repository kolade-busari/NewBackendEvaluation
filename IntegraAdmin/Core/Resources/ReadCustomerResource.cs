using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace IntegraAdmin.Core.Resources
{
    public class ReadCustomerResource
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }
        public string Country { get; set; }

        public SponsorResource Sponsor { get; set; }
        public int SponsorId { get; set; }

        public ICollection<ProductResource> Products { get; set; }

        public ReadCustomerResource()
        {
            Products = new Collection<ProductResource>();
        }
    }
}
