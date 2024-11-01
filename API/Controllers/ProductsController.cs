using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IGenericRepository<Product> productRepo;
    private readonly IGenericRepository<ProductBrand> productBrandRepo;
    private readonly IGenericRepository<ProductType> productTypeRepo;
    private readonly IMapper mapper;

    public ProductsController(
        IGenericRepository<Product> productRepo,
        IGenericRepository<ProductBrand> productBrandRepo,
        IGenericRepository<ProductType> productTypeRepo,
        IMapper mapper)
    {
        this.productRepo = productRepo;
        this.productBrandRepo = productBrandRepo;
        this.productTypeRepo = productTypeRepo;
        this.mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
    {
        var spec = new ProductsWithTypesAndBrandsSpecification();
        var products = await productRepo.FindAsync(spec);
        return Ok(mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
    {
        return Ok(await productBrandRepo.FindAllAsync());
    }


    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
    {
        return Ok(await productTypeRepo.FindAllAsync());
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(id);
        var product = await productRepo.FindEntityWithSpec(spec);
        return Ok(mapper.Map<Product, ProductToReturnDto>(product));
    }
}