using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using FoodIstAPI.Application.DTOs.Order;
using FoodIstAPI.Application.DTOs.User;

namespace FoodIstAPI.Infrastructure.Mappings;

/// <summary>
/// [Mapper] for { AddUser <=> Dto, User <=> Dto};
/// </summary>
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<AddUserDto, User>().ReverseMap();
        CreateMap<UserDto, User>().ReverseMap();
    }
}