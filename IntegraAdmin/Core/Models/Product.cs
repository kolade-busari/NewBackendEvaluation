using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntegraAdmin.Core.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string ProductName { get; set; }

        public string Description { get; set; }

        public ICollection<CustomerProduct> Customers { get; set; }
        public Product()
        {
            Customers = new Collection<CustomerProduct>();
        }
    }
}
