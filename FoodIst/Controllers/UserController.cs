using AutoMapper;
using FoodIstAPI.Application.DTOs.User;
using FoodIstAPI.Application.Interfaces;
using FoodIstAPI.Infrastructure.Mappings;
using Microsoft.AspNetCore.Mvc;

namespace FoodIst.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    // SERVICES.
    private readonly IUserService _service;

    // INITIALIZATION.
    public UserController(IUserService service) => _service = service;

    [HttpGet("All")]
    public async Task<IActionResult> GetALl()
    {
        var users = await _service.GetUsers();
        return Ok(users);
    }


    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.RemoveUser(id);
        return Ok();
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update(UpdateUserDto userDto)
    {
        await _service.UpdateUser(userDto);
        return Ok();
    }
}
