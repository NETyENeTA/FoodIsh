using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

/// <summary>
/// Order [CLASS]: Needs to create a Order which have Items and User, who ordered this order
/// </summary>
public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<OrderItem> Items { get; set; }
    public User User { get; set; }
}
