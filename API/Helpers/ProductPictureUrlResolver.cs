using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers;

public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
{
    private readonly IConfiguration configuration;

    public ProductPictureUrlResolver(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.PictureUrl))
        {
            return configuration.GetValue<string>("ApiUrl") + source.PictureUrl;
        }

        return null;
    }
}
