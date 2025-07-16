using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodIstAPI.Application.DTOs.Order;

/// <summary>
/// [INFO] Item Data Transfer.
/// </summary>
public class OrderItemDto
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}