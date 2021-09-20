using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.API.Products.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerce.API.Products.Interfaces
{
    public class CustomerProvider : ICustomersProvider
    {
        private readonly CustomersDbContext _context;
        private readonly ILogger<CustomerProvider> _logger;
        private readonly IMapper _mapper;

        public CustomerProvider(CustomersDbContext context, ILogger<CustomerProvider> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;

            SeedData();
        }

        private void SeedData() 
        {
            if (!_context.Customers.Any())
            {
                _context.Customers.Add(new Customer() { Id = 1, Name = "John Doe", Address = "1833 route aha, Longueuil QC" });
                _context.Customers.Add(new Customer() { Id = 2, Name = "Anthony Depp", Address = "185 route oho, Longueuil QC" });
                _context.Customers.Add(new Customer() { Id = 3, Name = "Michelle Swiffer", Address = "13 route ihi, Montreal QC" });
                _context.Customers.Add(new Customer() { Id = 4, Name = "Homer Doh", Address = "" });

                _context.SaveChanges();
            }
        }

        public async Task<(bool isSuccess, IEnumerable<Models.CustomerModel> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                var customers = await _context.Customers.ToListAsync();
                if (customers?.Any() ?? false)
                {
                    var results = _mapper.Map<IEnumerable<Customer>, IEnumerable<Models.CustomerModel>>(customers);
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

        public async Task<(bool isSuccess, Models.CustomerModel Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var customer = await _context.Customers.FirstOrDefaultAsync(p => p.Id == id);
                if (customer != null)
                {
                    var result = _mapper.Map<Customer, Models.CustomerModel>(customer);
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
