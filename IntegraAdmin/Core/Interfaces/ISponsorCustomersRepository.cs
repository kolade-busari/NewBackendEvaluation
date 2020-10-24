using IntegraAdmin.Core.Models;
using IntegraAdmin.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegraAdmin.Core.Interfaces
{
    public interface ISponsorCustomersRepository
    {
        Task<List<Customer>> AllCustomers(string sponsorUserId);
        Task<Customer> GetCustomer(string sponsorUserId, int id);
        Task AddCustomer(string sponsorUserId, Customer customer);

        Task UpdateCustomer(string sponsorUserId, SaveCustomerResource resource, Customer customer);
    }
}
