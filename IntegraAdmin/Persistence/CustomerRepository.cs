using IntegraAdmin.Core.Interfaces;
using IntegraAdmin.Core.Models;
using IntegraAdmin.Core.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegraAdmin.Persistence
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddCustomer(Customer customer)
        {
            _context.Customers.AddAsync(customer);
        }

        public async Task<List<Customer>> AllCustomers()
        {
            return await _context.Customers
                        .Include(x => x.Sponsor)
                        .Include(x => x.Products)
                            .ThenInclude(x => x.Product)
                        .ToListAsync();
        }

        public async Task<Customer> GetCustomer(int id)
        {
            return await _context.Customers
                        .Include(x => x.Sponsor)
                        .Include(x => x.Products)
                            .ThenInclude(x => x.Product)
                        .SingleOrDefaultAsync(x => x.Id == id);
        }

        public void RemoveCustomer(Customer customer)
        {
            _context.Customers.Remove(customer);
        }

        public void UpdateCustomer(SaveCustomerResource resource, Customer customer)
        {

            // update fields
            customer.Update(resource);

            // remove product no selected
            foreach (var customerProduct in customer.Products.ToList())
            {
                if (!resource.Products.Contains(customerProduct.ProductId))
                {
                    var product = customer.Products.Where(x => x.ProductId == customerProduct.ProductId).SingleOrDefault();
                    customer.Products.Remove(product);
                }
            }

            // add product
            foreach (var productId in resource.Products.ToList())
            {
                if(!customer.Products.Select(x => x.ProductId).Contains(productId))
                {
                    customer.Products.Add(new CustomerProduct { ProductId = productId });
                };
            }
        }
    }
}
