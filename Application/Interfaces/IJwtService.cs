using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FoodIstAPI.Application.Interfaces;

public interface IJwtService
{
    public Task<string> GetToken(List<Claim> claims);
}
