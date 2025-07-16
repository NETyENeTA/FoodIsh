using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodIstAPI.Application.DTOs.User;

/// <summary>
/// [INFO] [AUTH] User auth data transfer.
/// </summary>
public class UserAuthDto
{
    public string Login { get; set; }
    public string Password { get; set; }
}
