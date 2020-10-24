using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IntegraAdmin.Core.Interfaces;
using IntegraAdmin.Core.Models;
using IntegraAdmin.Core.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntegraAdmin.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomersController(IMapper mapper, ICustomerRepository vehicleRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _customerRepository = vehicleRepository;
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] SaveCustomerResource customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var modelCustomer = _mapper.Map<SaveCustomerResource, Customer>(customer);

            _customerRepository.AddCustomer(modelCustomer);
            await _unitOfWork.CompleteAsync();

            modelCustomer = await _customerRepository.GetCustomer(modelCustomer.Id);

            return Ok(_mapper.Map<Customer, ReadCustomerResource>(modelCustomer));
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            List<Customer> customers = await _customerRepository.AllCustomers();
            var resources = _mapper.Map<List<Customer>, List<ReadCustomerResource>>(customers);

            return Ok(resources);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] SaveCustomerResource customerResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customer = await _customerRepository.GetCustomer(id);

            if (customer == null)
                return NotFound();

            _customerRepository.UpdateCustomer(customerResource, customer);

            await _unitOfWork.CompleteAsync();

            customer = await _customerRepository.GetCustomer(id);
            var result = _mapper.Map<Customer, ReadCustomerResource>(customer);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _customerRepository.GetCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }

            // delete the customer
            _customerRepository.RemoveCustomer(customer);
            await _unitOfWork.CompleteAsync();

            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _customerRepository.GetCustomer(id);

            if (customer == null)
            {
                return NotFound();
            }

            var customerResource = _mapper.Map<Customer, ReadCustomerResource>(customer);

            return Ok(customerResource);
        }

    }
}
