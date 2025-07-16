using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodIstAPI.Application.DTOs.User;

/// <summary>
/// [INFO] User data updater and transfer.
/// </summary>
public class UpdateUserDto
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Role { get; set; }
}
