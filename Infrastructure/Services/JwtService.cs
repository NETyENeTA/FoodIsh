using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using FoodIstAPI.Application.DTOs.Order;
using FoodIstAPI.Application.Interfaces;
using FoodIstAPI.Application.Settings;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FoodIstAPI.Infrastructure.Services;

/// <summary>
/// [CLASS] [with-Interface] JWT Servise, can give Token, and save it.
/// </summary>
public class JwtService : IJwtService
{

    private readonly JwtSettings _settings;

    public JwtService(IOptions<JwtSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task<string> GetToken(List<Claim> claims)
    {
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);


        var jwt = new JwtSecurityToken(
            issuer: _settings.Issuer,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(Convert.ToInt32(_settings.Expires))),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

}