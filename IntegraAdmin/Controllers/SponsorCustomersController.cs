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
    [ApiController]
    public class SponsorCustomersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISponsorCustomersRepository _sponsorCustomersRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SponsorCustomersController(IMapper mapper, ISponsorCustomersRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _sponsorCustomersRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }


        [Authorize(Roles = "Admin, Sponsor Write")]
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] SaveCustomerResource customer)
        {
            string userId = User.Claims.First(c => c.Type == "UserId").Value;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var modelCustomer = _mapper.Map<SaveCustomerResource, Customer>(customer);

            await _sponsorCustomersRepository.AddCustomer(userId, modelCustomer);
            await _unitOfWork.CompleteAsync();

            modelCustomer = await _sponsorCustomersRepository.GetCustomer(userId, modelCustomer.Id);

            return Ok(_mapper.Map<Customer, ReadCustomerResource>(modelCustomer));
        }

        [Authorize(Roles = "Admin, Sponsor, Sponsor Read")]
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            string userId = User.Claims.First(c => c.Type == "UserId").Value;
            List<Customer> customers = await _sponsorCustomersRepository.AllCustomers(userId);
            var resources = _mapper.Map<List<Customer>, List<ReadCustomerResource>>(customers);

            return Ok(resources);
        }

        [Authorize(Roles = "Admin, Sponsor Write")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] SaveCustomerResource customerResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string userId = User.Claims.First(c => c.Type == "UserId").Value;

            var customer = await _sponsorCustomersRepository.GetCustomer(userId, id);

            if (customer == null)
                return NotFound();

            
            await _sponsorCustomersRepository.UpdateCustomer(userId, customerResource, customer);

            await _unitOfWork.CompleteAsync();

            customer = await _sponsorCustomersRepository.GetCustomer(userId, id);
            var result = _mapper.Map<Customer, ReadCustomerResource>(customer);

            return Ok(result);
        }

        [Authorize(Roles = "Admin, Sponsor, Sponsor Read")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            string userId = User.Claims.First(c => c.Type == "UserId").Value;

            var customer = await _sponsorCustomersRepository.GetCustomer(userId, id);

            if (customer == null)
            {
                return NotFound();
            }

            var customerResource = _mapper.Map<Customer, ReadCustomerResource>(customer);

            return Ok(customerResource);
        }

    }
}
