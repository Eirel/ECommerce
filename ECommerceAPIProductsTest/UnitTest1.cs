using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.API.Products.Db;
using ECommerce.API.Products.Interfaces;
using ECommerce.API.Products.Profiles;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ECommerceAPIProductsTest
{
    public class ProductServiceTest
    {
        [Fact]
        public async Task GetProductsReturnsAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts))
                .Options;
            var context = new ProductsDbContext(options);
            CreateProducts(context);

            var productProfile = new ProductProfile();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(mapperConfiguration);

            var provider = new ProductsProvider(context, null, mapper);

            var results = await provider.GetProductsAsync();

            Assert.True(results.isSuccess);
            Assert.True(results.Products.Any());
            Assert.Null(results.ErrorMessage);
        }

        [Fact]
        public async Task GetProductsReturnProductWithValidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnProductWithValidId))
                .Options;
            var context = new ProductsDbContext(options);
            CreateProducts(context);

            var productProfile = new ProductProfile();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(mapperConfiguration);

            var provider = new ProductsProvider(context, null, mapper);

            var results = await provider.GetProductAsync(1);

            Assert.True(results.isSuccess);
            Assert.NotNull(results.Product);
            Assert.Equal(1, results.Product.Id);
            Assert.Null(results.ErrorMessage);
        }

        [Fact]
        public async Task GetProductsReturnProductWithInvalidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnProductWithValidId))
                .Options;
            var context = new ProductsDbContext(options);
            CreateProducts(context);

            var productProfile = new ProductProfile();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(mapperConfiguration);

            var provider = new ProductsProvider(context, null, mapper);

            var results = await provider.GetProductAsync(-1);

            Assert.False(results.isSuccess);
            Assert.Null(results.Product);
            Assert.NotNull(results.ErrorMessage);
        }

        private void CreateProducts(ProductsDbContext context) {
            for (var i = 1; i < 10; i++) {
                context.Products.Add(new Product()
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = i + 10,
                    Price = (decimal)(i * 3.14)
                });
            }
            context.SaveChanges();
        }
    }
}
