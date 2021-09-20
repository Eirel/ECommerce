using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.API.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider _provider;
        public OrdersController(IOrdersProvider provider)
        {
            _provider = provider;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersAsync()
        {
            var results = await _provider.GetOrdersAsync();
            if (results.isSuccess)
            {
                return Ok(results.Orders);
            }
            return NotFound();
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrderAsync(int customerId)
        {
            var result = await _provider.GetOrdersAsync(customerId);
            if (result.isSuccess)
            {
                return Ok(result.Orders);
            }
            return NotFound();
        }
    }
}
