using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodIstAPI.Application.DTOs.Order;

/// <summary>
/// [INFO] for Creating order.
/// </summary>
public class CreateOrderDto
{
    public int UserId { get; set; }
    public List<OrderItemDto> Items { get; set; }
}