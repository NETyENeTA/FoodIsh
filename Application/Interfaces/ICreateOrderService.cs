using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodIstAPI.Application.DTOs.Order;

namespace FoodIstAPI.Application.Interfaces;

public interface IOrderService
{
    Task<List<CreateOrderDto>> GetAllAsync();
    Task AddAsync(CreateOrderDto productDto);
}
