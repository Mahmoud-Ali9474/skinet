using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> genericRepository;
        private readonly IMapper mapper;

        public ProductsController(IGenericRepository<Product> genericRepository, IMapper mapper)
        {
            this.genericRepository = genericRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            //throw new Exception("uihkj");
            var spec = new ProductsWithTypesAndBrandsSpecification();
            
            var products = await genericRepository.ListAsync(spec);

            return Ok(mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await genericRepository.GetEntityWithSpecAsync(spec);
            var x = product.Id;
            return Ok(mapper.Map<Product, ProductToReturnDto>(product));
        }
    }
}