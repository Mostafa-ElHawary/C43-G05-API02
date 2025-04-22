using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence
{
    public class  DbInitializer : IDbInitializer
    {
        private readonly StoreDbContext _context;
        public DbInitializer(StoreDbContext context) {

            _context = context;
        }
        public async Task InitializeAsync()
        {

            try
            {
                if (_context.Database.GetPendingMigrations().Any())
                {
                    await _context.Database.MigrateAsync();
                }

                if (!_context.ProductTypes.Any())
                {
                    var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    if (types != null && types.Any())
                    {
                         _context.ProductTypes.AddRange(types);
                        await _context.SaveChangesAsync();
                    }

                }


                if (!_context.ProductBrands.Any())
                {

                    var BrandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\brands.json");
                    var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                    if (Brands != null && Brands.Any())
                    {
                         _context.ProductBrands.AddRange(Brands);
                        await _context.SaveChangesAsync();
                    }

                }


                if (!_context.Products.Any())
                {

                    var ProductsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\products.json");
                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                    if (Products != null && Products.Any())
                    {
                         _context.Products.AddRange(Products);
                        await _context.SaveChangesAsync();
                    }

                }

            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
