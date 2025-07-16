using FluentValidation;
using FoodIstAPI.Application.DTOs.User;
using FoodIstAPI.Application.Interfaces;
using FoodIstAPI.Application.Settings;
using FoodIstAPI.Application.Validators;
using FoodIstAPI.Infrastructure.Services;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

/// Start Pint of builder.
var builder = WebApplication.CreateBuilder(args);

/// Services.
// DB Context:
builder.Services.AddDbContext<AppDbContext>(options =>
   options.UseSqlite("Data Source=shop.db"));

// IServices:
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IOrderService, OrderService>();

// JWT:
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddScoped<IJwtService, JwtService>();

// Validator: [Not Working]
//builder.Services.AddScoped<IValidator<AddUserDto>, AddUserDtoValidator>();

// Controllers, End-Points, Swagger-Gen:
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Mapper:
builder.Services.AddAutoMapper(cfg => cfg.AddMaps("FoodIstAPI.Infrastructure"));

/// Building APP.
var app = builder.Build();

/// Migrations.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

/// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/// Somthing
app.MapControllers();

/// Uses
app.UseHttpsRedirection();
app.UseAuthorization();

/// Running APP, Start Point
app.Run();
