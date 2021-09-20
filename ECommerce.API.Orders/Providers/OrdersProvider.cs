using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.API.Orders.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerce.API.Orders.Interfaces
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrdersDbContext _context;
        private readonly ILogger<OrdersProvider> _logger;
        private readonly IMapper _mapper;

        public OrdersProvider(OrdersDbContext context, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!_context.Orders.Any())
            {
                _context.Orders.Add(new Order() { Id = 1, CustomerId = 2, OrderDate = DateTime.Now,
                    Items = new List<OrderItem> {
                        new OrderItem() { Id = 1, ProductId = 2, Quantity = 5, UnitPrice = 5 },
                        new OrderItem() { Id = 4, ProductId = 4, Quantity = 2, UnitPrice = 20 }
                    }
                });
                _context.Orders.Add(new Order() { Id = 2, CustomerId = 1, OrderDate = DateTime.Now.AddDays(1),
                    Items = new List<OrderItem> {
                        new OrderItem() { Id = 2, ProductId = 1, Quantity = 1, UnitPrice = 20 },
                        new OrderItem() { Id = 3, ProductId = 3, Quantity = 2, UnitPrice = 150 }
                    }
                });

                _context.SaveChanges();
            }
        }

        public async Task<(bool isSuccess, IEnumerable<Models.OrderModel> Orders, string ErrorMessage)> GetOrdersAsync()
        {
            try
            {
                var orders = await _context.Orders
                    .Include(o => o.Items).ToListAsync();
                if (orders?.Any() ?? false)
                {
                    var results = _mapper.Map<IEnumerable<Order>, IEnumerable<Models.OrderModel>>(orders);
                    return (true, results, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool isSuccess, IEnumerable<Models.OrderModel> Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                var orders = await _context.Orders
                    .Include(o => o.Items).Where(p => p.CustomerId == customerId).ToListAsync();
                if (orders?.Any() ?? false)
                {
                    var result = _mapper.Map<IEnumerable<Order>, IEnumerable<Models.OrderModel>>(orders);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
