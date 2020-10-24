using IntegraAdmin.Core.Models;
using IntegraAdmin.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegraAdmin.Core.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> AllCustomers();
        Task<Customer> GetCustomer(int id);
        void AddCustomer(Customer customer);
        void RemoveCustomer(Customer customer);

        void UpdateCustomer(SaveCustomerResource resource, Customer customer);
    }
}
