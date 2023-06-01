using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    var brandData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                    await context.ProductBrands.AddRangeAsync(brands);
                    await context.SaveChangesAsync();
                }
                if (!context.ProductTypes.Any())
                {
                    var typeData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);
                    await context.ProductTypes.AddRangeAsync(types);
                    await context.SaveChangesAsync();
                }
                if (!context.Products.Any())
                {
                    var productData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");

                    var proudcts = JsonSerializer.Deserialize<List<Product>>(productData);
                    await context.Products.AddRangeAsync(proudcts);
                    await context.SaveChangesAsync();
                }


            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}