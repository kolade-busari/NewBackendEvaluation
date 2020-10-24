using IntegraAdmin.Core.Interfaces;
using IntegraAdmin.Core.Models;
using IntegraAdmin.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegraAdmin.Core.Resources
{
    public class SponsorCustomersRepository : ISponsorCustomersRepository
    {
        private readonly ApplicationDbContext _context;

        public SponsorCustomersRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddCustomer(string sponsorUserId, Customer customer)
        {
            // get the sponsor
            ApplicationUser user = await _context.Users.FindAsync(sponsorUserId);
            customer.SponsorId = user.SponsorId;

            await _context.Customers.AddAsync(customer);
        }

        public async Task<List<Customer>> AllCustomers(string sponsorUserId)
        {
            // get the sponsor
            ApplicationUser user = await _context.Users.FindAsync(sponsorUserId);

            return await _context.Customers
                        .Where(x => x.SponsorId == user.SponsorId)
                        .Include(x => x.Sponsor)
                        .Include(x => x.Products)
                            .ThenInclude(x => x.Product)
                        .ToListAsync();
        }

        public async Task<Customer> GetCustomer(string sponsorUserId, int id)
        {
            // get the sponsor
            ApplicationUser user = await _context.Users.FindAsync(sponsorUserId);

            return await _context.Customers
                        .Include(x => x.Sponsor)
                        .Include(x => x.Products)
                            .ThenInclude(x => x.Product)
                        .SingleOrDefaultAsync(x => x.Id == id && x.SponsorId == user.SponsorId);
        }


        public async Task UpdateCustomer(string sponsorUserId, SaveCustomerResource resource, Customer customer)
        {
            // get the sponsor
            ApplicationUser user = await _context.Users.FindAsync(sponsorUserId);

            // update the sponsor Id
            resource.SponsorId = user.SponsorId;

            // update fields
            customer.Update(resource);

            // remove product not selected
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
                if (!customer.Products.Select(x => x.ProductId).Contains(productId))
                {
                    customer.Products.Add(new CustomerProduct { ProductId = productId });
                };
            }
        }
    }
}
