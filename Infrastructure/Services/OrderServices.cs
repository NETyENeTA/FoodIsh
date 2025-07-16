using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using FoodIstAPI.Application.DTOs.Order;
using FoodIstAPI.Application.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FoodIstAPI.Infrastructure.Services;

/// <summary>
/// [CLASS] [with-Interface] Just CRUD for Orders
/// </summary>
public class OrderService : IOrderService
{
    // DateBase Context
    private readonly AppDbContext _context;

    /// <summary>
    /// Get All Orders
    /// </summary>
    /// <returns>List<CreateOrderDto></returns>
    public async Task<List<CreateOrderDto>> GetAllAsync()
    {
        // Reformat Data
        return await _context.Orders.Select(o => new CreateOrderDto()
        {
            UserId = o.UserId,
            Items = (List<OrderItemDto>)o.Items,
        }).ToListAsync();

    }

    /// <summary>
    /// Add Orders, Create Order.
    /// </summary>
    /// <param name="createOrderDto">OrderDto</param>
    /// <returns>Nothing or Exceptions</returns>
    public async Task AddAsync(CreateOrderDto createOrderDto)
    {
        // Check User, without same name in DateBase.
        User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == createOrderDto.UserId);
        if (user == default) return;

        // Getting IDes.
        IEnumerable<int> productIds = createOrderDto.Items.Select(i => i.ProductId).Distinct();
        IEnumerable<int> orderIds = createOrderDto.Items.Select(i => i.OrderId).Distinct();

        // Load all important (needs) products and orders by 1 call.
        Dictionary<int, Product> products = await _context.Products
            .Where(p => productIds.Contains(p.Id))
            .ToDictionaryAsync(p => p.Id);
        Dictionary<int, Order> orders = await _context.Orders
            .Where(o => orderIds.Contains(o.Id))
            .ToDictionaryAsync(o => o.Id);

        // Create Order
        var order = new Order()
        {
            UserId = createOrderDto.UserId,
            Items = createOrderDto.Items
                //Reformat Data
                .Where(i => products.ContainsKey(i.ProductId))
                .Where(i => orders.ContainsKey(i.OrderId))
                .Select(i => new OrderItem()
                {
                    OrderId = i.OrderId,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Product = products[i.ProductId],
                    Order = orders[i.OrderId]
                })
                .ToList(),
            User = user
        };

        // Adding and Saving
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Deleting By Id
    /// </summary>
    /// <param name="id">[int] Parameter</param>
    /// <returns>[bool] parameter {is [Order] deleted?}</returns>
    public async Task<bool> DeleteAsync(int id)
    {
        // Checking: is this order exist? No => Nothing
        Order? order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        if (order == default) return false;

        // Yes => Remove and saving
        _context.Remove(order);
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Deleting by Expression
    /// </summary>
    /// <param name="func">lymbda function [Expression]</param>
    /// <returns>[bool] parameter {is [Order] deleted?}</returns>
    public async Task<bool> DeleteAsync(Expression<Func<Order, bool>> func)
    {
        // Same check and actions
        Order? order = await _context.Orders.FirstOrDefaultAsync(func);
        if (order == null) return false;
        _context.Remove(order);
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Update Info about Order
    /// </summary>
    /// <param name="id">[int] Parameter (OrderID)</param>
    /// <param name="createOrderDto">OrderFto</param>
    /// <returns>[bool] parameter {is [Order] updated?</returns>
    public async Task<bool> Update(int id, CreateOrderDto createOrderDto)
    {
        // Check Order (we need to have 1 with that ID)
        Order? order = await GetAsync(id);
        if (order == null) return false;

        // Check User (without same name)
        User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == createOrderDto.UserId);
        if (user == null) return false;

        // Need to calculate (IDes)

        IEnumerable<int> productIds = createOrderDto.Items.Select(i => i.ProductId).Distinct();
        IEnumerable<int> orderIds = createOrderDto.Items.Select(i => i.OrderId).Distinct();

        Dictionary<int, Product> products = await _context.Products
            .Where(p => productIds.Contains(p.Id))
            .ToDictionaryAsync(p => p.Id);

        Dictionary<int, Order> orders = await _context.Orders
            .Where(o => orderIds.Contains(o.Id))
            .ToDictionaryAsync(o => o.Id);

        /// should We use a Mapper?

        order.UserId = createOrderDto.UserId;
        //order.CreatedAt = // Need Something here?

        order.Items = createOrderDto.Items
                .Where(i => products.ContainsKey(i.ProductId))
                .Where(i => orders.ContainsKey(i.OrderId))
                .Select(i => new OrderItem()
                {
                    OrderId = i.OrderId,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Product = products[i.ProductId],
                    Order = orders[i.OrderId]
                })
                .ToList();
        order.User = user;

        /// Mapper?

        return true;
    }

    /// <summary>
    /// Gets Order by id
    /// </summary>
    /// <param name="id">[int] parameter</param>
    /// <returns>Order?</returns>
    public async Task<Order?> GetAsync(int id)
    {
        return await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
    }
    /// <summary>
    /// Gets order by Expression
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public async Task<Order?> GetAsync(Expression<Func<Order, bool>> func)
    {
        return await _context.Orders.FirstOrDefaultAsync(func);
    }



}