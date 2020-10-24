using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IntegraAdmin.Core.Interfaces;
using IntegraAdmin.Core.Models;
using IntegraAdmin.Core.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IntegraAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepo;
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IMapper mapper, IProductRepository productRepo, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _productRepo = productRepo;
            _unitOfWork = unitOfWork;
        }
        
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepo.AllProducts();
            var productsResource = _mapper.Map<List<Product>, List<ProductResource>>(products);
            return Ok(productsResource);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productRepo.GetProduct(id);

            if (product == null)
                return NotFound();

            var productResource = _mapper.Map<Product, ProductResource>(product);
            return Ok(productResource);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductResource productRes)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _productRepo.GetProduct(id);

            if (product == null)
                return NotFound();

            // update the product
            _mapper.Map<ProductResource, Product>(productRes, product);
            await _unitOfWork.CompleteAsync();

            var result = _mapper.Map<Product, ProductResource>(product);

            return Ok(result);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductResource productResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = _mapper.Map<ProductResource, Product>(productResource);

            _productRepo.AddProduct(product);
            await _unitOfWork.CompleteAsync();

            var result = _mapper.Map<Product, ProductResource>(product);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _productRepo.GetProduct(id);

            if (product == null)
                return NotFound();

            // delete the product
            _productRepo.RemoveProduct(product);
            await _unitOfWork.CompleteAsync();

            return Ok(id);
        }
    }
}
