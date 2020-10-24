using IntegraAdmin.Core.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntegraAdmin.Core.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string CompanyName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Country { get; set; }

        public Sponsor Sponsor { get; set; }
        public int SponsorId { get; set; }

        public ICollection<CustomerProduct> Products { get; set; }

        public Customer()
        {
            Products = new Collection<CustomerProduct>();
        }

        internal void Update(SaveCustomerResource customerResource)
        {
            CompanyName = customerResource.CompanyName;
            Email = customerResource.Email;
            Address = customerResource.Address;
            Country = customerResource.Country;
            SponsorId = customerResource.SponsorId;
        }
    }
}
