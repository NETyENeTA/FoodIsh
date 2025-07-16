using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodIstAPI.Application.DTOs.Order;

namespace FoodIstAPI.Application.Interfaces;

public interface IProductService
{
    Task<List<ProductDto>> GetAllAsync();
    Task AddAsync(ProductDto productDto);
    Task Update(ProductDto productDto);
    Task Remove(int id);
    Task<ProductDto?> GetById(int id);
}
