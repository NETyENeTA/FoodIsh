using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Domain.Entities;
using FoodIstAPI.Application.Interfaces;

using Infrastructure.Data;
using System.Linq.Expressions;
using FoodIstAPI.Application.DTOs.User;
using AutoMapper;

namespace FoodIstAPI.Infrastructure.Services;

/// <summary>
/// [CLASS] [with-Interface] Just CRUD for Orders
/// </summary>
public class UserService : IUserService
{
    // DB Context.
    private readonly AppDbContext _context;

    // Mapper
    private readonly IMapper _mapper;

    // INITIALISATION
    public UserService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Authorize Account
    /// </summary>
    /// <param name="userAuthDto">User Data Authentication</param>
    /// <returns>UserDto</returns>
    /// <exception cref="Exception"></exception>
    public async Task<UserDto> AuthorizeUser(UserAuthDto userAuthDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == userAuthDto.Login);

        if (user == default)
            throw new Exception($"User under login{userAuthDto.Login} was not found");

        if (!BCrypt.Net.BCrypt.Verify(userAuthDto.Password, user.PasswordHash))
            throw new Exception($"Entered [WRONG] password");

        return _mapper.Map<UserDto>(user);
    }


    /// <summary>
    /// Gets All Users/Accounts
    /// </summary>
    /// <returns>IEnumerable<UserDto></returns>
    public async Task<IEnumerable<UserDto>> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        return _mapper.Map<List<UserDto>>(users);
    }

    /// <summary>
    /// Gets User by ID
    /// </summary>
    /// <param name="id">[int] parameter</param>
    /// <returns>UserDto</returns>
    /// <exception cref="Exception">User was not found</exception>
    public async Task<UserDto> GetUserById(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync();

        if (user == default)
            throw new Exception($"User with id:{id} wasn't found");

        return _mapper.Map<UserDto>(user);
    }

    /// <summary>
    /// Adding User into DateBase.
    /// </summary>
    /// <param name="addUserDto">User Data Transfer</param>
    /// <returns>UserID, which was created.</returns>
    /// <exception cref="Exception">This user is already exists</exception>
    public async Task<int> AddUser(AddUserDto addUserDto)
    {
        if (_context.Users.Any(u => u.Login == addUserDto.Login))
            throw new Exception($"User under login:{addUserDto.Login} is already exist");

        var user = _mapper.Map<User>(addUserDto);

        user.Role = "Customer";
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(addUserDto.Password);

        await _context.AddAsync(user);
        await _context.SaveChangesAsync();

        return user.Id;
    }

    /// <summary>
    /// Update User Info
    /// </summary>
    /// <param name="updateUserDto">User new INFO, but with same id</param>
    /// <returns>Nothing or Exceptions</returns>
    /// <exception cref="Exception">User wasn't found</exception>
    public async Task UpdateUser(UpdateUserDto updateUserDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == updateUserDto.Id);

        if (user == default)
            throw new Exception($"User with id:{updateUserDto.Id} was not found");

        user.Login = updateUserDto.Login;
        user.Role = updateUserDto.Role;

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Remove User by his ID
    /// </summary>
    /// <param name="id">[int] parameter</param>
    /// <returns>Nothing or Exception</returns>
    /// <exception cref="Exception">User wasn't found</exception>
    public async Task RemoveUser(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user == default)
            throw new Exception($"User with id:{id} was not found");

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}