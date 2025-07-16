using System.Security.Claims;
using FluentValidation;
using FoodIstAPI.Application.DTOs.User;
using FoodIstAPI.Application.Interfaces;
using FoodIstAPI.Application.Validators;
using FoodIstAPI.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodIst.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    // SERVICES
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;
    //private readonly IValidator<AddUserDto> _userValidator; /// IDK :L

    // INITIALIZATION
    public AuthController(IUserService userService, IJwtService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
    }

    /// <summary>
    /// Simple Account Registration
    /// </summary>
    /// <param name="user">AccountDTO</param>
    /// <returns>[int] UserID or Exeptions</returns>
    [HttpPost("Registrate")]
    public async Task<ActionResult<int>> Registry([FromBody] AddUserDto user)
    {
        // Get UserID, if operation has errors, so it throws new Exceptions
        var userId = await _userService.AddUser(user);

        // It's not working with error ending
        return Ok(userId);
    }

    /// <summary>
    /// Login Account
    /// </summary>
    /// <param name="user">AccountDTO</param>
    /// <returns>IActionResult or Exeptions</returns>
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] UserAuthDto user)
    {
        // User authorization
        var userDto = await _userService.AuthorizeUser(user);
        if (userDto == null)
            return Unauthorized();


        // Creating claims for a token
        var claims = new List<Claim>
            {
                new (ClaimTypes.Name, userDto.Login),
                new (ClaimTypes.Role, userDto.Role)
            };

        // Generate Token (with save)
        var token = _jwtService.GetToken(claims);
        return Ok(new { Token = token });

    }


}
