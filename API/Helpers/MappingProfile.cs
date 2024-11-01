using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductToReturnDto>()
            .ForMember(p => p.ProductType, o => o.MapFrom(s => s.ProductType.Name))
            .ForMember(p => p.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
            .ForMember(p => p.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());
    }
}
