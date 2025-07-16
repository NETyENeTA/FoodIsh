using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FoodIstAPI.Application.DTOs.User;

namespace FoodIstAPI.Application.Validators;

/// <summary>
/// Validator, checks and validate with Adding User
/// </summary>
public class AddUserDtoValidator : AbstractValidator<AddUserDto>
{
// How To Add/Connect/Use This? Tried by Internet :L
    public AddUserDtoValidator()
    {
        // Check [Login] with rules: NOT NULL AND .length>4;
        RuleFor(x => x.Login)
            .NotEmpty().WithMessage("[Required] Login")
            .MinimumLength(4).WithMessage("The username must be at least 4 characters long");

        // Check [Password] with rules: NOT NULL AND .length>6;
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("[Required] Password")
            .MinimumLength(6).WithMessage("The password must be at least 6 characters long");

        // Check [Role] with rules: NOT NULL AND Role('Admin' or 'Customer');
        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("[Required] Role")
            .Must(role => role == "Admin" || role == "Customer")
            .WithMessage("Роль должна быть либо 'Admin', либо 'Customer'");
    }
}