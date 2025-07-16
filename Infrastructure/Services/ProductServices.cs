using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Domain.Entities;
using FoodIstAPI.Application.Interfaces;

using Infrastructure.Data;
using System.Linq.Expressions;
using FoodIstAPI.Application.DTOs.Order;
using AutoMapper;

namespace FoodIstAPI.Infrastructure.Services;
/// <summary>
/// [CLASS] [with-Interface] Just CRUD for Product(s)
/// </summary>
public class ProductService : IProductService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ProductService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ProductDto>> GetAllAsync()
    {
        var entities = await _context.Products.ToListAsync();
        return _mapper.Map<List<ProductDto>>(entities);
    }

    public async Task AddAsync(ProductDto productDto)
    {
        if (await _context.Products.FirstOrDefaultAsync(p => p.Name == productDto.Name) != default)
            throw new Exception($"Product with name:{productDto.Name} is already exist");

        var entity = _mapper.Map<Product>(productDto);
        await _context.Products.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(ProductDto productDto)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Name == productDto.Name);

        if (product == default)
            throw new Exception($"Product with name:{productDto.Name} didn't find");

        _mapper.Map(productDto, product);
        await _context.SaveChangesAsync();
    }

    public async Task Remove(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

        if (product == default)
            throw new Exception($"Product with id:{id} didn't find");

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task<ProductDto?> GetById(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

        if (product == default)
            throw new Exception($"Product with id:{id} didn't find");

        return _mapper.Map<ProductDto>(product);
    }
}