using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntegraAdmin.Core.Models
{
    public class Sponsor
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string CompanyName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Country { get; set; }

        public ICollection<Customer> Customers { get; set; }
        public Sponsor()
        {
            Customers = new Collection<Customer>();
        }

    }
}
