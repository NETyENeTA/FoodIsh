using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodIstAPI.Application.DTOs.Order;

/// <summary>
/// [INFO] Product Data Transfer
/// </summary>
public class ProductDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Amount { get; set; }
}