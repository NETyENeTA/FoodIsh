﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodIstAPI.Application.DTOs.User;

/// <summary>
/// [INFO] Adding User
/// </summary>
public class AddUserDto
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}
