using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProductsController : BaseApiController
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
    public async Task<ActionResult<Pagination<Product>>> GetProducts(
        [FromQuery] ProductSpecParams specParams
    )
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(specParams);
        var countSpec = new ProductsWithFiltersForCountSpecification(specParams);
        var products = await productRepo.FindAsync(spec);
        var totalItemsCount = await productRepo.CountAsync(countSpec);
        var productDtos = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
        var pagination = new Pagination<ProductToReturnDto>
        {
            PageIndex = specParams.PageIndex,
            PageSize = specParams.PageSize,
            TotalItemsCount = totalItemsCount,
            Data = productDtos
        };
        return Ok(pagination);
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
    [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(id);
        var product = await productRepo.FindEntityWithSpec(spec);
        if (product == null)
        {
            return NotFound(new ApiResponse(StatusCodes.Status404NotFound));
        }
        return Ok(mapper.Map<Product, ProductToReturnDto>(product));
    }
}