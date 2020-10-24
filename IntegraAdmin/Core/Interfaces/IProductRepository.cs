using IntegraAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegraAdmin.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> AllProducts();
        Task<Product> GetProduct(int id);
        void AddProduct(Product product);
        void RemoveProduct(Product product);
    }
}
