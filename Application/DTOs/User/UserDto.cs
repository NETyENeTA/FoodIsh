using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodIstAPI.Application.DTOs.User;

/// <summary>
/// [INFO] Transfer Data
/// </summary>
public class UserDto
{
    public int id { get; set; }
    public string Login { get; set; }
    public string Role { get; set; }
}
