using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRespository : IProductRepository
{
    private readonly StoreContext _storeContext;
    public ProductRespository(StoreContext storeContext)
    {
        _storeContext = storeContext;

    }

    public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
    {
        return await _storeContext
            .ProductBrands
            .ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _storeContext
            .Products
            .Include(p => p.ProductBrand)
            .Include(p => p.ProductType)
            .SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        return await _storeContext
            .Products
            .Include(p => p.ProductBrand)
            .Include(p => p.ProductType)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
    {
        return await _storeContext
            .ProductTypes
            .ToListAsync();
    }
}
