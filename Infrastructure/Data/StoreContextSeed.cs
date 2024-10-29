using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;

public class StoreContextSeed
{
    public async static Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            #region Seed Brands

            if (!context.ProductBrands.Any())
            {
                var brandData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

                await context.ProductBrands.AddRangeAsync(brands);
            }
            #endregion

            #region Seed Types

            if (!context.ProductTypes.Any())
            {
                var typeData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);

                await context.ProductTypes.AddRangeAsync(types);
            }
            #endregion

            #region Seed Products

            if (!context.Products.Any())
            {
                var productData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productData);

                await context.Products.AddRangeAsync(products);
            }
            #endregion


            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {

            var logger = loggerFactory.CreateLogger<StoreContextSeed>();
            logger.LogError(ex, "Error occured during seed data.");
        }
    }
}
