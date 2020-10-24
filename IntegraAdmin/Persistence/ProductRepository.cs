using IntegraAdmin.Core.Interfaces;
using IntegraAdmin.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegraAdmin.Persistence
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddProduct(Product product)
        {
            _context.Products.AddAsync(product);
        }

        public async Task<List<Product>> AllProducts()
        {
            return await _context.Products.Include(x => x.Customers).ToListAsync();
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public void RemoveProduct(Product product)
        {
            _context.Products.Remove(product);
        }
    }
}
