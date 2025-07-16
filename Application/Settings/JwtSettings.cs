using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodIstAPI.Application.Settings;

/// <summary>
/// Jwt Kit Settings, it create [CLASS] For Token, nut it is a 'Token'.
/// </summary>
public class JwtSettings
{
    // Who own.
    public string Issuer { get; set; }
    // When Created or to delete.
    public int Expires { get; set; }
    // Token key.
    public string Key { get; set; }
}
