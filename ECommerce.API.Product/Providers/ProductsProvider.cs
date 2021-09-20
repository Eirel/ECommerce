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
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext _context;
        private readonly ILogger<ProductsProvider> _logger;
        private readonly IMapper _mapper;

        public ProductsProvider(ProductsDbContext context, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;

            SeedData();
        }

        private void SeedData() 
        {
            if (!_context.Products.Any())
            {
                _context.Products.Add(new Db.Product() { Id = 1, Name = "Keyboard", Price = 20, Inventory = 100 });
                _context.Products.Add(new Db.Product() { Id = 2, Name = "Mouse", Price = 5, Inventory = 15 });
                _context.Products.Add(new Db.Product() { Id = 3, Name = "Monitor", Price = 150, Inventory = 10 });
                _context.Products.Add(new Db.Product() { Id = 4, Name = "CPU", Price = 20, Inventory = 150 });

                _context.SaveChanges();
            }
        }

        public async Task<(bool isSuccess, IEnumerable<Models.ProductModel> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await _context.Products.ToListAsync();
                if (products?.Any() ?? false)
                {
                    var results = _mapper.Map<IEnumerable<Db.Product>, IEnumerable<Models.ProductModel>>(products);
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

        public async Task<(bool isSuccess, Models.ProductModel Product, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                {
                    var result = _mapper.Map<Db.Product, Models.ProductModel>(product);
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
