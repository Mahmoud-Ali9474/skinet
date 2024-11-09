using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications;

public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
{
    public ProductsWithTypesAndBrandsSpecification(ProductSpecParams specParams)
    : base(
        p => (!specParams.BrandId.HasValue || p.ProductBrandId == specParams.BrandId)
        && (!specParams.TypeId.HasValue || p.ProductTypeId == specParams.TypeId)
        && (string.IsNullOrEmpty(specParams.SearchTerm) || p.Name.ToLower().Contains(specParams.SearchTerm))
    )
    {
        AddInclude(p => p.ProductType);
        AddInclude(p => p.ProductBrand);
        //AddOrderBy(p => p.Name);
        ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

        switch (specParams.Sort)
        {
            case "priceAsc":
                AddOrderBy(p => p.Price);
                break;
            case "priceDesc":
                AddOrderByDesending(p => p.Price);
                break;
            default:
                AddOrderBy(p => p.Name);
                break;
        }
    }
    public ProductsWithTypesAndBrandsSpecification(int id) : base(p => p.Id == id)
    {
        AddInclude(p => p.ProductType);
        AddInclude(p => p.ProductBrand);
    }
}
