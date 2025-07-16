using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using FoodIstAPI.Application.DTOs.Order;

namespace FoodIstAPI.Infrastructure.Mappings;

/// <summary>
/// [Mapper] for { Product <=> Dto };
/// </summary>
public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
    }
}