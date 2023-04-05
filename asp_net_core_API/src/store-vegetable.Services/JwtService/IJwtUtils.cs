using store_vegetable.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace store_vegetable.Services.JwtService
{
    public interface IJwtUtils
    {
        string GenerateToken(User user);
        int? ValidateToken(string Token);
    }
}
