using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

/// <summary>
/// User [CLASS]: Needs to create a User.
/// </summary>
public class User
{
    public int Id { get; set; }
    public string Login { get; set; }

    // We save hashed password, not it self, becouse it will protecly
    public string PasswordHash { get; set; }
    // We don't have Types of Roles :(
    public string Role { get; set; }
}