using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.API.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Products.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsProvider _provider;
        public ProductsController(IProductsProvider provider)
        {
            _provider = provider;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var results = await _provider.GetProductsAsync();
            if (results.isSuccess)
            {
                return Ok(results.Products);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var result = await _provider.GetProductAsync(id);
            if (result.isSuccess)
            {
                return Ok(result.Product);
            }
            return NotFound();
        }
    }
}
