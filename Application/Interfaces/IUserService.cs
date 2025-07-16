using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodIstAPI.Application.DTOs.User;

namespace FoodIstAPI.Application.Interfaces;

public interface IUserService
{
    /// <summary>
    /// Authorize the user
    /// </summary>
    /// <param name="userAuthDto">Authorization module</param>
    /// <returns>UserDto</returns>
    Task<UserDto> AuthorizeUser(UserAuthDto userAuthDto);

    /// <summary>
    /// Getl All Users
    /// </summary>
    /// <returns>IEnumerate<UserDto></returns>
    Task<IEnumerable<UserDto>> GetUsers();

    /// <summary>
    /// Get User By ID
    /// </summary>
    /// <param name="id">[int:parameter]</param>
    /// <returns>UserDto</returns>
    Task<UserDto> GetUserById(int id);

    /// <summary>
    /// Adding User
    /// </summary>
    /// <param name="addUserDto">User Model [Mode:adding]</param>
    /// <returns> [int:parameter] UserId</returns>
    Task<int> AddUser(AddUserDto addUserDto);

    /// <summary>
    /// Update User Info
    /// </summary>
    /// <param name="updateUserDto">User Model [Mode:Update]</param>
    /// <returns>Nothing or Exceptions</returns>
    Task UpdateUser(UpdateUserDto updateUserDto);

    /// <summary>
    /// Remove User from DateBase
    /// </summary>
    /// <param name="id">[int:parameter]</param>
    /// <returns>Nothing or Exceptions</returns>
    Task RemoveUser(int id);
}

