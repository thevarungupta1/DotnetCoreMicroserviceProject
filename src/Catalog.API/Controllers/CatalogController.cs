using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _repository.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = await _repository.GetProduct(id);
            if (product == null)
            {
                _logger.LogError($"Product with id: {id}, not found.");
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet]
        [Route("[action]/{category}", Name = "GetProductByCategory")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
        {
            var product = await _repository.GetProductByCategory(category);
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await _repository.CreateProduct(product);
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            return Ok(await _repository.UpdateProduct(product));
        }


        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            return Ok(await _repository.DeleteProduct(id));
        }

    }
}
