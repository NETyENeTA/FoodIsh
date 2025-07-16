using FoodIstAPI.Application.DTOs.Order;
using FoodIstAPI.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodIst.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    // SERVICES.
    readonly IProductService _service;

    // INITIALIZATION.
    public ProductController(IProductService service) => _service = service;

    /// <summary>
    /// Adding Product into DateBase.
    /// </summary>
    /// <param name="product">ProductDto</param>
    /// <returns>IActionResult or Exceptions</returns>
    [HttpPost("Add")]
    public async Task<IActionResult> Add(ProductDto product)
    {
        // Adding user, with excepting errors.
        await _service.AddAsync(product);

        // if it has errors, it no execute.
        return Ok();
    }

    /// <summary>
    /// Gets all products, what we have.
    /// </summary>
    /// <returns>IEnumerable<ProductDto>, it can be empty</returns>
    [HttpGet("Get/All")]
    public async Task<IEnumerable<ProductDto>> GetAll()
        => await _service.GetAllAsync();

    /// <summary>
    /// Just update product in DB
    /// </summary>
    /// <param name="product">ProductDto</param>
    /// <returns>IActionResult or Exceptions</returns>
    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] ProductDto product)
    {
        // Updating User, with excepting errors.
        await _service.Update(product);

        // if it has errors, it no execute.
        return Ok();
    }

    /// <summary>
    /// Remove Product by id
    /// </summary>
    /// <param name="id">[int] Parameter</param>
    /// <returns>IActionResult or Exceptions</returns>
    [HttpDelete("Remove")]
    public async Task<IActionResult> Remove(int id)
    {
        // Delete User by ID, with excepting errors.
        await _service.Remove(id);

        // if it has errors, it no execute.
        return Ok();
    }

    /// <summary>
    /// Get Product by id
    /// </summary>
    /// <param name="id">[int] Parameter</param>
    /// <returns>IActionResult or Exceptions</returns>
    [HttpGet("Get")]
    public async Task<ProductDto?> GetById(int id)
    {
        // Getting user by id with servise, which excepting errors.
        var product = await _service.GetById(id);

        // if it has errors, it no execute.
        return product;
    }
}