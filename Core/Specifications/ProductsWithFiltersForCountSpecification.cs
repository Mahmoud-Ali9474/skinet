using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications;

public class ProductsWithFiltersForCountSpecification : BaseSpecification<Product>
{
    public ProductsWithFiltersForCountSpecification(ProductSpecParams specParams)
        : base(
            p => (!specParams.BrandId.HasValue || p.ProductBrandId == specParams.BrandId)
                && (!specParams.TypeId.HasValue || p.ProductTypeId == specParams.TypeId)
                && (string.IsNullOrEmpty(specParams.SearchTerm) || p.Name.ToLower().Contains(specParams.SearchTerm))
        )
    {

    }

}
